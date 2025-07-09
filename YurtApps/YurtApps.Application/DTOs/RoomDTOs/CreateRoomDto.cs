namespace YurtApps.Application.DTOs.RoomDTOs
{
    public class CreateRoomDto
    {
        public short RoomNumber { get; set; }
        public short RoomCapacity { get; set; }
        public int DormitoryId { get; set; }
    }
}