using Microsoft.AspNetCore.Identity;
using YurtApps.Application.DTOs.DormitoryDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Services
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public DormitoryService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task CreateDormitoryAsync(CreateDormitoryDto dto, string userId)
        {
            var dormitory = new Dormitory
            {
                DormitoryName = dto.DormitoryName,
                DormitoryAddress = dto.DormitoryAddress,
                UserId = userId
            };

            await _unitOfWork.Repository<Dormitory>().CreateAsync(dormitory);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteDormitoryAsync(int dormitoryId, string userId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dormitoryId);

            if (dormitory == null || dormitory.UserId != userId)
                throw new KeyNotFoundException("Dormitory not found");

            var users = await _unitOfWork.Repository<User>().GetAllAsync();

            var usersInDormitory = users.Where(u => u.DormitoryId == dormitoryId).ToList();

            foreach (var user in usersInDormitory)
            {
                await _userManager.DeleteAsync(user);
            }

            await _unitOfWork.Repository<Dormitory>().DeleteAsync(dormitoryId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultDormitoryDto>> GetAllDormitoryAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var rooms = await _unitOfWork.Repository<Room>().GetAllAsync();

            if (roles.Contains("Admin"))
            {
                var dormitories = await _unitOfWork.Repository<Dormitory>().GetAllAsync();
                return dormitories
                    .Where(d => d.UserId == userId)
                    .Select(d => new ResultDormitoryDto
                    {
                        DormitoryId = d.DormitoryId,
                        DormitoryName = d.DormitoryName,
                        DormitoryAddress = d.DormitoryAddress,
                        DormitoryCapacity = (short)rooms
                            .Where(r => r.DormitoryId == d.DormitoryId)
                            .Sum(r => r.RoomCapacity)
                    }).ToList();
            }

            else
            {
                if (user.DormitoryId == null)
                    return new List<ResultDormitoryDto>();

                var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(user.DormitoryId.Value);

                if (dormitory == null)
                    return new List<ResultDormitoryDto>();

                return new List<ResultDormitoryDto>
                {
                    new ResultDormitoryDto
                    {
                        DormitoryId = dormitory.DormitoryId,
                        DormitoryName = dormitory.DormitoryName,
                         DormitoryCapacity = (short)rooms
                            .Where(r => r.DormitoryId == dormitory.DormitoryId)
                            .Sum(r => r.RoomCapacity),
                        DormitoryAddress = dormitory.DormitoryAddress
                    }
                };
            }
        }

        public async Task<ResultDormitoryDto?> GetDormitoryByIdAsync(int id, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return null;

            var dorm = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(id);
            if (dorm == null) 
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin") && dorm.UserId == userId)
            {
                return new ResultDormitoryDto
                {
                    DormitoryId = dorm.DormitoryId,
                    DormitoryName = dorm.DormitoryName,
                    DormitoryCapacity = (short)(dorm.Rooms?.Sum(x => x.RoomCapacity) ?? 0),
                    DormitoryAddress = dorm.DormitoryAddress
                };
            }

            if (roles.Contains("User") && user.DormitoryId == id)
            {
                return new ResultDormitoryDto
                {
                    DormitoryId = dorm.DormitoryId,
                    DormitoryName = dorm.DormitoryName,
                    DormitoryCapacity = (short)(dorm.Rooms?.Sum(x => x.RoomCapacity) ?? 0),
                    DormitoryAddress = dorm.DormitoryAddress
                };
            }
            return null;
        }

        public async Task UpdateDormitoryAsync(UpdateDormitoryDto dto, string userId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dto.DormitoryId);
            if (dormitory == null || dormitory.UserId != userId)
                throw new UnauthorizedAccessException();

            dormitory.DormitoryName = dto.DormitoryName;
            dormitory.DormitoryAddress = dto.DormitoryAddress;

            await _unitOfWork.CommitAsync();
        }
    }
}