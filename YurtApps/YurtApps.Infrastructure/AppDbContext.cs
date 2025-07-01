using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public AppDbContext() : base("AppDbContext")
        {

        }
        public DbSet<Dormitory> Dormitory { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DormitoryEntityConfigurations());
            modelBuilder.Configurations.Add(new RoomEntityConfigurations());
            modelBuilder.Configurations.Add(new StudentEntityConfigurations());
            modelBuilder.Configurations.Add(new UserEntityConfigurations());
        }

    }
}