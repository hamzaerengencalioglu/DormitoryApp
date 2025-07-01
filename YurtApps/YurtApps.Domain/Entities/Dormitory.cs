namespace YurtApps.Domain.Entities
{
    public class Dormitory
    {
        public int DormitoryId { get; set; }
        public string DormitoryName { get; set; }
        public short DormitoryCapacity { get; set; }
        public string DormitoryAddress { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual User User { get; set; }

    }
}
