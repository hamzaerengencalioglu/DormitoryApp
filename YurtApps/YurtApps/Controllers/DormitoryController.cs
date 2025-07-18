﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.API.Controllers
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

        [HttpPost]
        public async Task<IActionResult> CreateDormitory([FromBody] Dormitory dormitory)
        {
            try
            {
                await _dormitoryService.CreateDormitoryAsync(dormitory);
                return Ok("Dormitory successfully added");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

        [HttpGet]
        public async Task<IActionResult> GetAllDormitory()
        {
            var result = await _dormitoryService.GetAllDormitoryAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDormitoryById(int id)
        {
            var result = await _dormitoryService.GetDormitoryByIdAsync(id);
            if (result == null)
                return NotFound("Dormitory not found");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDormitoryAsync([FromBody] Dormitory dormitory)
        {
            try
            {
                await _dormitoryService.UpdateDormitoryAsync(dormitory);
                return Ok("Dormitory succesfully updated");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
