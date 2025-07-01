using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class DormitoryEntityConfigurations:EntityTypeConfiguration<Dormitory>
    {
        public DormitoryEntityConfigurations()
        {
            HasKey(d => d.DormitoryId);
            
                Property(d => d.DormitoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                Property(d => d.DormitoryCapacity)
                    .IsRequired();

                Property(d => d.DormitoryAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                HasRequired(u => u.User)
                    .WithMany(d => d.Dormitories)
                    .HasForeignKey(u => u.UserId);
        }
    }
}
