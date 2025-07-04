using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;
using YurtApps.Infrastructure.DataAccess;

namespace YurtApps.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dormitory> Dormitory { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new DormitoryEntityConfigurations());
            modelBuilder.ApplyConfiguration(new RoomEntityConfigurations());
            modelBuilder.ApplyConfiguration(new StudentEntityConfigurations());
            modelBuilder.ApplyConfiguration(new UserEntityConfigurations());
        }

    }
}