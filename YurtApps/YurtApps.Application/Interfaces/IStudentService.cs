using YurtApps.Application.DTOs.StudentDTOs;

namespace YurtApps.Application.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudentAsync(CreateStudentDto dto, string userId);
        Task DeleteStudentAsync(int studentId, string userId);
        Task<List<ResultStudentDto>> GetAllStudentAsync(string userId);
        Task<ResultStudentDto?> GetStudentByIdAsync(int studentId, string userId);
        Task UpdateStudentAsync(UpdateStudentDto dto, string userId);
    }
}