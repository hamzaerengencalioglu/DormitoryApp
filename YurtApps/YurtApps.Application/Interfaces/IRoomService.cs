using YurtApps.Application.DTOs.RoomDTOs;

namespace YurtApps.Application.Interfaces
{
    public interface IRoomService
    {
        Task<List<ResultRoomDto>> GetAllRoomAsync(string UserId);
        Task<List<ResultRoomDto>> GetRoomByDormitoryIdAsync(int DormitoryId, string UserId);
        Task CreateRoomAsync(CreateRoomDto dto, string userId);
        Task UpdateRoomAsync(UpdateRoomDto dto);
        Task DeleteRoomAsync(int RoomId);
    }
}