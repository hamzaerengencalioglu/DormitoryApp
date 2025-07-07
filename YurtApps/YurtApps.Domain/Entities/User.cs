using Microsoft.AspNetCore.Identity;

namespace YurtApps.Domain.Entities
{
    public class User : IdentityUser
    {
        public int? DormitoryId { get; set; }
        public virtual ICollection<Dormitory> Dormitories { get; set; } = new List<Dormitory>();
    }
}
