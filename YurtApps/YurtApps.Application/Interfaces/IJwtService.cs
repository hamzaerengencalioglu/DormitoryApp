using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(User user);
    }

}
