using YurtApps.Application.Dtos.UserDtos;

namespace YurtApps.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(CreateUserDto dto, string currentUserId);
    }
}
