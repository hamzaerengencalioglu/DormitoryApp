using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.RoomDTOs;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IRoomService
    {
        Task<List<ResultRoomDto>> GetAllRoomAsync();
        Task<ResultRoomDto> GetRoomByIdAsync(int RoomId);
        Task CreateRoomAsync(CreateRoomDto dto);
        Task UpdateRoomAsync(UpdateRoomDto dto);
        Task DeleteRoomAsync(int RoomId);
    }
}
