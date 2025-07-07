using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.DTOs.DormitoryDTOs;
using YurtApps.Application.Interfaces;

namespace YurtApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DormitoryController : ControllerBase
    {
        private readonly IDormitoryService _dormitoryService;

        public DormitoryController(IDormitoryService dormitoryService)
        {
            _dormitoryService = dormitoryService;
        }

        [Authorize(Policy = "CanWrite")]
        [HttpPost]
        public async Task<IActionResult> CreateDormitory(CreateDormitoryDto dto)
        {
            try
            {
                await _dormitoryService.CreateDormitoryAsync(dto);
                return Ok("Dormitory successfully added");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "OwnsDormitory")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDormitory(int id)
        {
            try
            {
                await _dormitoryService.DeleteDormitoryAsync(id);
                return Ok("Dormitory successfully deleted");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "CanRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllDormitory()
        {
            var result = await _dormitoryService.GetAllDormitoryAsync();
            return Ok(result);
        }

        [Authorize(Policy = "OwnsDormitory")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDormitoryById(int id)
        {
            var result = await _dormitoryService.GetDormitoryByIdAsync(id);
            if (result == null)
                return NotFound("Dormitory not found");

            return Ok(result);
        }

        [Authorize(Policy = "OwnsDormitory")]
        [HttpPut]
        public async Task<IActionResult> UpdateDormitoryAsync(UpdateDormitoryDto dto)
        {
            try
            {
                await _dormitoryService.UpdateDormitoryAsync(dto);
                return Ok("Dormitory succesfully updated");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
