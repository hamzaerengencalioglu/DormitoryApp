using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YurtApps.Application.DTOs.UserDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<User> userManager, 
                                          SignInManager<User> signInManager, 
                                          IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName
            };

            var result = await _userManager.CreateAsync(user, dto.UserPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Admin");

            await _userManager.AddClaimAsync(user, new Claim("Permission", "Read"));
            await _userManager.AddClaimAsync(user, new Claim("Permission", "Write"));

            return Ok("Registration successful");
        }

        [HttpPost("Login")]
        public  async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Unauthorized("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.UserPassword, false);
            if(!result.Succeeded)
                return Unauthorized("Wrong password");

            var token = await _jwtService.GenerateTokenAsync(user);
            return Ok(new { token });
        }

    }
}
