using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(VebTechDbContext context) : base(context)
        {

        }

        public async Task<Role> GetByName(string name)
        {
            var role = await _set
                .SingleOrDefaultAsync(q => q.Name == name);

            if (role is null)
            {
                throw new ObjectNotFoundException("Role with this name is not found");
            }

            return role;
        }

        public async Task<List<Role>> SortByName()
        {
            return await _set
                .OrderBy(q => q.Name)
                .ToListAsync();
        }
    }
}
