namespace YurtApps.Application.DTOs.StudentDTOs
{
    public class CreateStudentDto
    {
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string StudentEmail { get; set; }
        public int RoomId { get; set; }
    }
}