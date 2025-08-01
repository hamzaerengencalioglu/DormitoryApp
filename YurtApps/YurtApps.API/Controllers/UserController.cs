﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.Dtos.UserDtos;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Unauthorized();

            try
            {
                var result = await _userService.CreateUserAsync(dto, currentUser.Id);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("User")]
        public async Task<IActionResult> GetMyUsers()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Unauthorized();

            var users = await _userService.GetResultUserAsync(currentUser.Id);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("User/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Unauthorized();

            try
            {
                await _userService.DeleteUserAsync(id, currentUser.Id);
                return Ok("User deleted successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("You are not allowed to delete this user.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}