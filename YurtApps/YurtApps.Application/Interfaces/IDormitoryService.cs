using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.DormitoryDTOs;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IDormitoryService
    {
        Task<List<ResultDormitoryDto>> GetAllDormitoryAsync();
        Task<ResultDormitoryDto> GetDormitoryByIdAsync(int DormitoryId);
        Task CreateDormitoryAsync(CreateDormitoryDto dto);
        Task UpdateDormitoryAsync(UpdateDormitoryDto dto);
        Task DeleteDormitoryAsync(int DormitoryId);
    }
}
