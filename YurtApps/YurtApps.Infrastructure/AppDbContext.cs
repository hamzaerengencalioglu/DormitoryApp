using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YurtApps.Domain.Entities;
using YurtApps.Infrastructure.DataAccess;

namespace YurtApps.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dormitory> Dormitory { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DormitoryEntityConfigurations());
            modelBuilder.ApplyConfiguration(new RoomEntityConfigurations());
            modelBuilder.ApplyConfiguration(new StudentEntityConfigurations());
        }
    }
}