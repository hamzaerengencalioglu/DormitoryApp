using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YurtApps.Application.DTOs.StudentDTOs
{
    public class GetStudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentPhoneNumber { get; set; }
    }
}
