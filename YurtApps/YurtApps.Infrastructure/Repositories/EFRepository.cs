using Microsoft.EntityFrameworkCore;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Infrastructure.Repositories
{
    public class EFRepository<T> : IEFRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public EFRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task CreateAsync(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _appDbContext.Set<T>().Remove(entity);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
