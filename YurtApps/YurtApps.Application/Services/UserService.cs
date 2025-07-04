using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.UserDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.UserPassword))
                throw new ArgumentException("Password cannot be empty.");

            var entity = new User
            {
                UserName = dto.UserName,
                UserPassword = dto.UserPassword
            };

            await _unitOfWork.Repository<User>().CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(UserId);

            if (user == null)
                throw new ArgumentException("No user found to be deleted");

            await _unitOfWork.Repository<User>().DeleteAsync(UserId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultUserDto>> GetAllUserAsync()
        {
            var list = await _unitOfWork.Repository<User>().GetAllAsync();
            return list.Select(u => new ResultUserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserPassword = u.UserPassword

            }).ToList();
        }

        public async Task<ResultUserDto> GetUserbyIdAsync(int UserId)
        {
            var entity = await _unitOfWork.Repository<User>().GetByIdAsync(UserId);
            if (entity == null)
                return null;
            return new ResultUserDto
            {
                UserId = entity.UserId,
                UserName = entity.UserName,
                UserPassword = entity.UserPassword
            };
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentException("No user found to be updated");

            if (string.IsNullOrWhiteSpace(dto.UserName))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.UserPassword))
                throw new ArgumentException("Password cannot be empty.");

            var entity = new User
            {
                UserId = dto.UserId,
                UserName = dto.UserName,
                UserPassword = dto.UserPassword
            };

            await _unitOfWork.Repository<User>().UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
