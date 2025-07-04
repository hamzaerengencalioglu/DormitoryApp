using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.StudentDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateStudentAsync(CreateStudentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.StudentName))
                throw new ArgumentException("Student name cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.StudentSurname))
                throw new ArgumentException("Student surname cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.StudentPhoneNumber))
                throw new ArgumentException("Student phone number cannot be empty.");

            var entity = new Student
            {
                StudentName = dto.StudentName,
                StudentSurname = dto.StudentSurname,
                StudentPhoneNumber = dto.StudentPhoneNumber
            };

            await _unitOfWork.Repository<Student>().CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteStudentAsync(int StudentId)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(StudentId);

            if (student == null)
                throw new ArgumentException("No student found to be deleted");

            await _unitOfWork.Repository<Student>().DeleteAsync(StudentId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultStudentDto>> GetAllStudentAsync()
        {
            
            var list = await _unitOfWork.Repository<Student>().GetAllAsync();
            return list.Select(s => new ResultStudentDto
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname,
                StudentPhoneNumber = s.StudentPhoneNumber
            }).ToList();
        }

        public async Task<ResultStudentDto> GetStudentByIdAsync(int StudentId)
        {
            var entity = await _unitOfWork.Repository<Student>().GetByIdAsync(StudentId);
            if (entity == null)
                return null;

            return new ResultStudentDto
            {
                StudentId = entity.StudentId,
                StudentName = entity.StudentName,
                StudentSurname = entity.StudentSurname,
                StudentPhoneNumber = entity.StudentPhoneNumber
            };
        }

        public async Task UpdateStudentAsync(UpdateStudentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("No student found to be updated");

            if (string.IsNullOrWhiteSpace(dto.StudentName))
                throw new ArgumentException("Student name cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.StudentSurname))
                throw new ArgumentException("Student surname cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.StudentPhoneNumber))
                throw new ArgumentException("Student phone number cannot be empty.");

            var entity = new Student
            {
                StudentId = dto.StudentId,
                StudentName = dto.StudentName,
                StudentSurname = dto.StudentSurname,
                StudentPhoneNumber = dto.StudentPhoneNumber
            };

            await _unitOfWork.Repository<Student>().UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
