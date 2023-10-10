using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRole> AddRoleToUser(string userEmail, string roleName);
    }
}
