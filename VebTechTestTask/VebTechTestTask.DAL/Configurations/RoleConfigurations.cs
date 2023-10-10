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
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(20);
            builder.HasMany(q => q.Users).WithMany(w => w.Roles).UsingEntity<UserRole>
                (                   
                    q => q
                        .HasOne(q => q.User)
                        .WithMany(w => w.UserRoles)
                        .HasForeignKey(q => q.UserId)
                        .OnDelete(DeleteBehavior.Cascade),
                    q => q
                        .HasOne(q => q.Role)
                        .WithMany(w => w.UserRoles)
                        .HasForeignKey(q => q.RoleId)
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
