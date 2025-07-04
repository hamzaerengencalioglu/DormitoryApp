using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.UserDTOs;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<ResultUserDto>> GetAllUserAsync();
        Task<ResultUserDto> GetUserbyIdAsync(int UserId);
        Task CreateUserAsync(CreateUserDto dto);
        Task UpdateUserAsync(UpdateUserDto dto);
        Task DeleteUserAsync(int UserId);
    }
}
