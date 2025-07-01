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
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateStudentAsync(Student student)
        {
            _appDbContext.Student.Add(student);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int StudentId)
        {
            var student = await GetStudentByIdAsync(StudentId);
            if (student != null) 
            {
                _appDbContext.Student.Remove(student);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            var list = await _appDbContext.Student.ToListAsync();
            return list;
        }

        public async Task<Student> GetStudentByIdAsync(int StudentId)
        {
            var student = await _appDbContext.Student.Where(s => s.StudentId == StudentId)
                .FirstOrDefaultAsync();
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _appDbContext.Entry(student).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
