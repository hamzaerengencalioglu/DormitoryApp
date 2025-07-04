using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class RoomEntityConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.RoomNumber)
                .IsRequired();

            builder.Property(r => r.RoomCapacity)
                .IsRequired();

            builder.HasOne(d => d.Dormitory)
                .WithMany(r => r.Rooms)
                .HasForeignKey(d => d.DormitoryId);
        }
    }
}
