using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.StudentDTOs;
using YurtApps.Domain.Entities;

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
