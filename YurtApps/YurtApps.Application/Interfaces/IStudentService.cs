using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetStudentByIdAsync(int StudentId);
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int StudentId);
    }
}
