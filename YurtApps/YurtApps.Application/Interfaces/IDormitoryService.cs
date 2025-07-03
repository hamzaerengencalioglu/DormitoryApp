using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IDormitoryService
    {
        Task<List<Dormitory>> GetAllDormitoryAsync();
        Task<Dormitory> GetDormitoryByIdAsync(int DormitoryId);
        Task CreateDormitoryAsync(Dormitory dormitory);
        Task UpdateDormitoryAsync(Dormitory dormitory);
        Task DeleteDormitoryAsync(int DormitoryId);
    }
}
