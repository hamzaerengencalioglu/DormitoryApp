namespace YurtApps.Application.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int DormitoryId { get; set; }
    }
}