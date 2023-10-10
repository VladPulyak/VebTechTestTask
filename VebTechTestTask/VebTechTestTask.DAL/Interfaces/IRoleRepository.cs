using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByName(string name);

        Task<List<Role>> SortByName();
    }
}
