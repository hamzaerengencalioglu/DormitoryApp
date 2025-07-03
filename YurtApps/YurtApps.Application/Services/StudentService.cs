using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Application.Services
{
    public class StudentService:IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateStudentAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentName))
                throw new ArgumentException("Student name cannot be empty.");

            if (string.IsNullOrWhiteSpace(student.StudentSurname))
                throw new ArgumentException("Student surname cannot be empty.");

            if (string.IsNullOrWhiteSpace(student.StudentPhoneNumber))
                throw new ArgumentException("Student phone number cannot be empty.");

            await _unitOfWork.Repository<Student>().CreateAsync(student);
        }

        public async Task DeleteStudentAsync(int StudentId)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(StudentId);

            if (student == null)
                throw new ArgumentException("No student found to be deleted");

            await _unitOfWork.Repository<Student>().DeleteAsync(StudentId);
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _unitOfWork.Repository<Student>().GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int StudentId)
        {
            return await _unitOfWork.Repository<Student>().GetByIdAsync(StudentId);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException("No student found to be updated");

            if (string.IsNullOrWhiteSpace(student.StudentName))
                throw new ArgumentException("Student name cannot be empty.");

            if (string.IsNullOrWhiteSpace(student.StudentSurname))
                throw new ArgumentException("Student surname cannot be empty.");

            if (string.IsNullOrWhiteSpace(student.StudentPhoneNumber))
                throw new ArgumentException("Student phone number cannot be empty.");

            await _unitOfWork.Repository<Student>().UpdateAsync(student);
        }
    }
}
