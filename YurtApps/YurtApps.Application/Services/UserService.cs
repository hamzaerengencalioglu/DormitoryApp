﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using YurtApps.Application.Dtos.UserDtos;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateUserAsync(CreateUserDto dto, string currentUserId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dto.DormitoryId);

            if (dormitory == null)
                throw new Exception("Dormitory not found.");

            if (dormitory.UserId != currentUserId)
                throw new UnauthorizedAccessException("You do not own the selected dormitory.");

            var newUser = new User
            {
                UserName = dto.UserName,
                DormitoryId = dto.DormitoryId
            };

            var result = await _userManager.CreateAsync(newUser, dto.UserPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(newUser, "User");
            await _userManager.AddClaimAsync(newUser, new Claim("Permission", "Read"));
            await _userManager.AddClaimAsync(newUser, new Claim("Permission", "Write"));

            return "User created successfully.";
        }

        public async Task<List<ResultUserDto>> GetResultUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
                throw new UnauthorizedAccessException("Only admins can view this data");

            var dormitories = await _unitOfWork.Repository<Dormitory>().GetAllAsync();

            var ownedDormitory = dormitories
                .Where(d => d.UserId == userId)
                .Select(d => d.DormitoryId)
                .ToList();

            var allUsers = _userManager.Users.ToList();

            var result = allUsers
                .Where(u => u.DormitoryId != null)
                .Where(u => ownedDormitory.Contains((int)u.DormitoryId))
                .Select(u => new ResultUserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    DormitoryId = u.DormitoryId.ToString()

                }).ToList();
            return result;

        }

        public async Task DeleteUserAsync(string userIdToDelete, string currentUserId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userIdToDelete.ToString());
            if (userToDelete == null)
                throw new Exception("User not found.");

            var dormitories = await _unitOfWork.Repository<Dormitory>().GetAllAsync();
            var ownedDormitory = dormitories
                .Where(d => d.UserId == currentUserId)
                .Select(d => d.DormitoryId)
                .ToList();

            if (!userToDelete.DormitoryId.HasValue ||
                !ownedDormitory.Contains(userToDelete.DormitoryId.Value))
            {
                throw new UnauthorizedAccessException("You cannot delete this user.");
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            if (!result.Succeeded)
                throw new Exception("Failed to delete the user.");
        }
    }
}