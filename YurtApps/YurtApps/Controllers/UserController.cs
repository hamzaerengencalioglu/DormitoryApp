using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return Ok("User successfully added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("User successfully deleted");
            }
            catch (Exception ex)
            {
                return NotFound("User not found");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUserAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserbyIdAsync(id);
            if (result == null)
                return Ok(result);
            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
                return Ok("User successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
