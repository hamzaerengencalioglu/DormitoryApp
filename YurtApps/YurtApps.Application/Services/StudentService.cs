using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YurtApps.Application.DTOs.StudentDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;
using YurtApps.Messaging.Contracts.Dtos;
using ZiggyCreatures.Caching.Fusion;

namespace YurtApps.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IPublishEndpoint _publish;
        private readonly IFusionCache _fusionCache;

        public StudentService(IUnitOfWork unitOfWork, UserManager<User> userManager, IPublishEndpoint publish, IFusionCache fusionCache)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _publish = publish;
            _fusionCache = fusionCache;
        }

        public async Task CreateStudentAsync(CreateStudentDto dto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? 
                throw new UnauthorizedAccessException();

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(dto.RoomId) ?? 
                throw new ArgumentException("Room not found");

            var dorm = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(room.DormitoryId) ?? 
                throw new ArgumentException("Dormitory not found");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin") && dorm.UserId != user.Id)
                throw new UnauthorizedAccessException();

            if (roles.Contains("User") && user.DormitoryId != dorm.DormitoryId)
                throw new UnauthorizedAccessException();

            var allStudents = await _unitOfWork.Repository<Student>().GetAllAsync();
            var currentCount = allStudents.Count(s => s.RoomId == room.RoomId);

            if (currentCount >= room.RoomCapacity)
                throw new InvalidOperationException("Room capacity is full.");

            var entity = new Student
            {
                StudentName = dto.StudentName,
                StudentSurname = dto.StudentSurname,
                StudentPhoneNumber = dto.StudentPhoneNumber,
                StudentEmail = dto.StudentEmail,
                RoomId = dto.RoomId
            };
            try
            {
                await _unitOfWork.Repository<Student>().CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                await _fusionCache.RemoveAsync($"students:all:{userId}");

                var mail = new MailDto
                {
                    To = dto.StudentEmail,
                    Subject = "Dormitory Registration Information",
                    Body = $"Hello {dto.StudentName}, your registration at {dorm.DormitoryName} dormitory has been successfully completed"
                };

                await _publish.Publish(mail);
            }

            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Student_StudentEmail") == true)
            {
                throw new InvalidOperationException("There is already a student registered with this email address.");
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Student_StudentPhoneNumber") == true)
            {
                throw new InvalidOperationException("There is already a student registered with this phone number.");
            }
        }

        public async Task DeleteStudentAsync(int studentId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? 
                throw new UnauthorizedAccessException();

            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentId) ?? 
                throw new ArgumentException("Student not found");

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(student.RoomId);
            var dorm = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(room.DormitoryId);

            await _unitOfWork.Repository<Student>().DeleteAsync(studentId);
            await _unitOfWork.CommitAsync();
            await _fusionCache.RemoveAsync($"students:all:{userId}");

            var mail = new MailDto
            {
                To = student.StudentEmail,
                Subject = "Dormitory Deregistration Information",
                Body = $"Hello {student.StudentName}, your registration at {dorm.DormitoryName} dormitory has been successfully deleted"
            };

            await _publish.Publish(mail);
        }

        public async Task<List<ResultStudentDto>> GetAllStudentAsync(string userId)
        {
            var cacheKey = $"students:all:{userId}";

            return await _fusionCache.GetOrSetAsync(cacheKey, async ctx =>
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return new List<ResultStudentDto>();

                var roles = await _userManager.GetRolesAsync(user);

                var allStudents = await _unitOfWork.Repository<Student>().GetAllAsync();
                var allRooms = await _unitOfWork.Repository<Room>().GetAllAsync();
                var allDorms = await _unitOfWork.Repository<Dormitory>().GetAllAsync();

                var result = new List<ResultStudentDto>();

                if (roles.Contains("Admin"))
                {
                    var adminDormIds = allDorms
                        .Where(d => d.UserId == user.Id)
                        .Select(d => d.DormitoryId)
                        .ToList();

                    var validRoomIds = allRooms
                        .Where(r => adminDormIds.Contains(r.DormitoryId))
                        .Select(r => r.RoomId)
                        .ToList();

                    foreach (var student in allStudents.Where(s => validRoomIds.Contains(s.RoomId)))
                    {
                        result.Add(new ResultStudentDto
                        {
                            StudentId = student.StudentId,
                            StudentName = student.StudentName,
                            StudentSurname = student.StudentSurname,
                            StudentPhoneNumber = student.StudentPhoneNumber,
                            StudentEmail = student.StudentEmail,
                            RoomId = student.RoomId
                        });
                    }
                }
                else if (roles.Contains("User") && user.DormitoryId != null)
                {
                    var validRoomIds = allRooms
                        .Where(r => r.DormitoryId == user.DormitoryId)
                        .Select(r => r.RoomId)
                        .ToList();

                    foreach (var student in allStudents.Where(s => validRoomIds.Contains(s.RoomId)))
                    {
                        result.Add(new ResultStudentDto
                        {
                            StudentId = student.StudentId,
                            StudentName = student.StudentName,
                            StudentSurname = student.StudentSurname,
                            StudentPhoneNumber = student.StudentPhoneNumber,
                            StudentEmail = student.StudentEmail,
                            RoomId = student.RoomId
                        });
                    }
                }

                return result;

            }, TimeSpan.FromHours(24));
        }

        public async Task<ResultStudentDto?> GetStudentByIdAsync(int studentId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentId);

            if (user == null || student == null) return null;

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(student.RoomId);
            var dorm = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(room.DormitoryId);
            var roles = await _userManager.GetRolesAsync(user);

            if ((roles.Contains("Admin") && dorm.UserId == user.Id) ||
                (roles.Contains("User") && user.DormitoryId == dorm.DormitoryId))
            {
                return new ResultStudentDto
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    StudentSurname = student.StudentSurname,
                    StudentPhoneNumber = student.StudentPhoneNumber,
                    StudentEmail = student.StudentEmail,
                    RoomId = student.RoomId
                };
            }

            return null;
        }

        public async Task UpdateStudentAsync(UpdateStudentDto dto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(dto.StudentId);

            if (user == null || student == null)
                throw new UnauthorizedAccessException();

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(dto.RoomId);

            if (room == null)
                throw new ArgumentException("Invalid RoomId.");

            var dorm = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(room.DormitoryId);

            if (dorm == null)
                throw new ArgumentException("Room does not belong to a valid dormitory.");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin") && dorm.UserId != user.Id)
                throw new UnauthorizedAccessException("You do not own this dormitory.");

            if (roles.Contains("User") && user.DormitoryId != dorm.DormitoryId)
                throw new UnauthorizedAccessException("You do not belong to this dormitory.");

            student.StudentName = dto.StudentName;
            student.StudentSurname = dto.StudentSurname;
            student.StudentPhoneNumber = dto.StudentPhoneNumber;
            student.StudentEmail = dto.StudentEmail;
            student.RoomId = dto.RoomId;

            await _unitOfWork.CommitAsync();
            await _fusionCache.RemoveAsync($"students:all:{userId}");
        }
    }
}