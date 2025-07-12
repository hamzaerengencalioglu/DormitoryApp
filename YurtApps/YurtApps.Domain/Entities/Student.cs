namespace YurtApps.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentPhoneNumber { get; set; }
        public int RoomId { get; set; }
        public string StudentEmail { get; set; }
        public virtual Room Room { get; set; }
 
    }
}