using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.Property(q => q.RefreshToken).IsRequired();
            builder.HasOne(q => q.User).WithOne(w => w.Token).HasForeignKey<UserToken>(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
