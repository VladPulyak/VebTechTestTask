using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace BusinessLayer.Services
{
    public static class StartupServices
    {
        public static async Task MigrateDatabase(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VebTechDbContext>();
                context.Database.Migrate();
                await RolesSeedData(context);
            }
        }

        private static async Task RolesSeedData(VebTechDbContext context)
        {
            if (!await context.Roles.AnyAsync())
            {
                await context.AddRangeAsync(new List<Role>()
                {
                    new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User"
                    },
                    new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Admin"
                    },
                    new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Support"
                    },
                    new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "SuperAdmin"
                    }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
