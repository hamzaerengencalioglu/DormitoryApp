namespace YurtApps.Application.DTOs.DormitoryDTOs
{
    public class UpdateDormitoryDto
    {
        public int DormitoryId { get; set; }
        public string DormitoryName { get; set; }
        public short DormitoryCapacity { get; set; }
        public string DormitoryAddress { get; set; }
    }
}
