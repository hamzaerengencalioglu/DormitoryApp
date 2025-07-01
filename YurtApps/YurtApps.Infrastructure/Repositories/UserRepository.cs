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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            _appDbContext.User.Add(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await GetUserbyIdAsync(UserId);
            if (user != null) 
            {
                _appDbContext.User.Remove(user);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var list = await _appDbContext.User.ToListAsync();
            return list;
        }

        public async Task<User> GetUserbyIdAsync(int UserId)
        {
            var user = await _appDbContext.User.Where(u => u.UserId == UserId)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _appDbContext.Entry(user).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
