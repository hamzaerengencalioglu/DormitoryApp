using YurtApps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace YurtApps.Infrastructure.DataAccess
{
    public class StudentEntityConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.StudentName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.StudentSurname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.StudentPhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(r => r.Room)
               .WithMany(s => s.Students)
               .HasForeignKey(r => r.RoomId);
        }
    }
}
