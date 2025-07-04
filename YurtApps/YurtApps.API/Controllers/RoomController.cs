using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.DTOs.RoomDTOs;
using YurtApps.Application.Interfaces;

namespace YurtApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto dto)
        {
            try
            {
                await _roomService.CreateRoomAsync(dto);
                return Ok("User successfully added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return Ok("Room successfully deleted");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoom()
        {
            var result = await _roomService.GetAllRoomAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await _roomService.GetRoomByIdAsync(id);
            if (result == null)
                return NotFound("Room not found");
            return Ok(result);
        }

        [HttpPut]
        public async Task <IActionResult> UpdateRoom([FromBody] UpdateRoomDto dto)
        {
            try
            {
                await _roomService.UpdateRoomAsync(dto);
                return Ok("Room succesfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
