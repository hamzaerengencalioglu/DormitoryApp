using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Domain.IRepositories
{
    public interface IDormitoryRepository
    {
        Task<List<Dormitory>> GetAllDormitoryAsync();
        Task<Dormitory> GetDormitoryByIdAsync(int Dormitoryd);
        Task CreateDormitoryAsync(Dormitory dormitory);
        Task UpdateDormitoryAsync(Dormitory dormitory);
        Task DeleteDormitoryAsync(int DormitoryId);
    }
}
