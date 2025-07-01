using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class StudentEntityConfigurations:EntityTypeConfiguration<Student>
    {
        public StudentEntityConfigurations()
        {
            HasKey(s => s.StudentId);

            Property(s => s.StudentName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.StudentSurname)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.StudentPhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(r => r.Room)
               .WithMany(s => s.Students)
               .HasForeignKey(r => r.RoomId);
        }
    }
}
