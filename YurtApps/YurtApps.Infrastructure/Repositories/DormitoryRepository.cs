using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Infrastructure.Repositories
{
    public class DormitoryRepository : IDormitoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public DormitoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateDormitoryAsync(Dormitory dormitory)
        {
            _appDbContext.Dormitory.Add(dormitory);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteDormitoryAsync(int DormitoryId)
        {
            var dormitory = await GetDormitoryByIdAsync(DormitoryId);
            if (dormitory != null)
            {
                _appDbContext.Dormitory.Remove(dormitory);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Dormitory>> GetAllDormitoryAsync()
        {
            var list = await _appDbContext.Dormitory.ToListAsync();
            return list;
        }

        public async Task<Dormitory> GetDormitoryByIdAsync(int dormitoryId)
        {
            var dormitory = await _appDbContext.Dormitory.Where(d => d.DormitoryId == dormitoryId)
                .FirstOrDefaultAsync();
            return dormitory;
        }

        public async Task UpdateDormitoryAsync(Dormitory dormitory)
        {
            _appDbContext.Entry(dormitory).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}