using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.Pagination;
using BusinessLayer.Dtos.Roles;
using DAL.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        Task<GetRoleByNameResponceDto> GetRoleByName(string name);

        Task<PagedResult<List<Role>>> SortRolesByName(int pageSize, int pageNumber);
    }
}
