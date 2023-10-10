using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BusinessLayer.Dtos.Pagination;
using BusinessLayer.Dtos.Roles;
using BusinessLayer.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BusinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<GetRoleByNameResponceDto> GetRoleByName(string name)
        {
            var role = await _roleRepository.GetByName(name);
            return _mapper.Map<GetRoleByNameResponceDto>(role);
        }

        public async Task<PagedResult<List<Role>>> SortRolesByName(int pageSize, int pageNumber)
        {
            var roles = await _roleRepository.SortByName();
            var pagedRoles = roles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<Role>>
            {
                Data = pagedRoles,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (roles.Count / pageSize) + 1
            };
        }
    }
}
