using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YurtApps.Application.Dtos.UserDtos;
using YurtApps.Domain.Entities;

namespace YurtApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.DormitoryId == null)
                return BadRequest("You cannot add users without creating a dormitory");

            var newUser = new User
            {
                UserName = dto.UserName,
                DormitoryId = currentUser.DormitoryId
            };

            var result = await _userManager.CreateAsync(newUser, dto.UserPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(newUser, dto.Role);
            await _userManager.AddClaimAsync(newUser, new Claim("Permission", "Read"));
            await _userManager.AddClaimAsync(newUser, new Claim("Permission", "Write"));

            return Ok("User created successfully");
        }
    }
}