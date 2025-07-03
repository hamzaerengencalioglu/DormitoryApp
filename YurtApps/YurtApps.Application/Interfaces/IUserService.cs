using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserbyIdAsync(int UserId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int UserId);
    }
}
