using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Configurations;
using DAL.Entities;

namespace DAL
{
    public class VebTechDbContext : DbContext
    {
        public VebTechDbContext()
        {

        }

        public VebTechDbContext(DbContextOptions<VebTechDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new UserRolesConfigurations());
            modelBuilder.ApplyConfiguration(new UserTokenConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
