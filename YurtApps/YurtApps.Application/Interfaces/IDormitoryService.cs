using YurtApps.Application.DTOs.DormitoryDTOs;

namespace YurtApps.Application.Interfaces
{
    public interface IDormitoryService
    {
        Task<List<ResultDormitoryDto>> GetAllDormitoryAsync(string UserId);
        Task<ResultDormitoryDto> GetDormitoryByIdAsync(int DormitoryId, string UserId);
        Task CreateDormitoryAsync(CreateDormitoryDto dto, string UserId);
        Task UpdateDormitoryAsync(UpdateDormitoryDto dto, string UserId);
        Task DeleteDormitoryAsync(int DormitoryId, string UserId);
    }
}
