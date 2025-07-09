using YurtApps.Application.DTOs.StudentDTOs;

namespace YurtApps.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<ResultStudentDto>> GetAllStudentAsync();
        Task<ResultStudentDto> GetStudentByIdAsync(int StudentId);
        Task CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(UpdateStudentDto dto);
        Task DeleteStudentAsync(int StudentId);
    }
}
