using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task CreateUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.UserPassword))
                throw new ArgumentException("Password cannot be empty.");

            await _unitOfWork.Repository<User>().CreateAsync(user);
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(UserId);

            if (user == null)
                throw new ArgumentException("No user found to be deleted");

            await _unitOfWork.Repository<User>().DeleteAsync(UserId);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();
        }

        public async Task<User> GetUserbyIdAsync(int UserId)
        {
            return await _unitOfWork.Repository<User>().GetByIdAsync(UserId);
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentException("No user found to be updated");

            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.UserPassword))
                throw new ArgumentException("Password cannot be empty.");

            await _unitOfWork.Repository<User>().UpdateAsync(user);
        }
    }
}
