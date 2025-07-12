using Microsoft.AspNetCore.Identity;
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
    }
}