using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DAL.Extensions;
using BusinessLayer.MapProfiles;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddDependencies(connectionString);
            services.AddAutoMapper(typeof(UserMapProfiles), typeof(RoleMapProfiles));
        }
    }
}
