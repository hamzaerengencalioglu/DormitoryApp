using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomAsync();
        Task<Room> GetRoomByIdAsync(int RoomId);
        Task CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int RoomId);
    }
}
