using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static void AddDbConnection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<VebTechDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        public static void AddDependencies(this IServiceCollection services, string connectionString)
        {
            AddDbConnection(services, connectionString);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        }
    }
}
