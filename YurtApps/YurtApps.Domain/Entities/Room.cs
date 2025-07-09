namespace YurtApps.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public short RoomNumber { get; set; }
        public short RoomCapacity { get; set; }
        public int DormitoryId { get; set; }
        public virtual Dormitory Dormitory { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
