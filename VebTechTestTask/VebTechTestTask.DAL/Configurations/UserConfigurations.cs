using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Age).IsRequired();
            builder.HasIndex(q => q.Email).IsUnique();
            builder.HasIndex(q => q.Login).IsUnique();
        }
    }
}
