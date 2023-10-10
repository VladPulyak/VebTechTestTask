using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BusinessLayer.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleService(IUserRoleRepository userRoleService, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _userRoleRepository = userRoleService;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<UserRole> AddRoleToUser(string userEmail, string roleName)
        {
            var user = await _userRepository.GetByEmail(userEmail);
            var role = await _roleRepository.GetByName(roleName);
            var addedUserRole = await _userRoleRepository.Add(new UserRole
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = role.Id,
                UserId = user.Id
            });
            await _userRoleRepository.Save();
            return addedUserRole;
        }
    }
}
