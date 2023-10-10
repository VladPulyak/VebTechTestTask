using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(VebTechDbContext context) : base(context)
        {

        }

        public async Task<List<UserRole>> GetByUserId(string userId)
        {
            return await _set
                .Where(q => q.UserId == userId)
                .Include(q => q.Role)
                .ToListAsync();
        }
    }
}
