using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;

namespace YurtApps.Infrastructure.DataAccess
{
    public class UserEntityConfigurations:EntityTypeConfiguration<User>
    {
        public UserEntityConfigurations()
        {
            HasKey(u => u.UserId);
            
            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.UserPassword)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
