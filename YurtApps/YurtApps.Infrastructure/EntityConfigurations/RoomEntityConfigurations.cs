using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class RoomEntityConfigurations:EntityTypeConfiguration<Room>
    {
        public RoomEntityConfigurations() 
        {
            HasKey(r => r.RoomId);

            Property(r => r.RoomNumber)
                .IsRequired();

            Property(r => r.RoomCapacity)
                .IsRequired();

            HasRequired(d => d.Dormitory)
                .WithMany(r => r.Rooms)
                .HasForeignKey(d => d.DormitoryId);
        }
    }
}
