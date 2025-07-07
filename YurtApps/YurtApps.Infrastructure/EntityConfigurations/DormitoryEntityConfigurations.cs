using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class DormitoryEntityConfigurations : IEntityTypeConfiguration<Dormitory>
    {
        public void Configure(EntityTypeBuilder<Dormitory> builder)
        {
            builder.HasKey(d => d.DormitoryId);

            builder.Property(d => d.DormitoryName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(d => d.DormitoryCapacity)
                    .IsRequired();

            builder.Property(d => d.DormitoryAddress)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.HasOne(u => u.User)
                    .WithMany(d => d.Dormitories)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
