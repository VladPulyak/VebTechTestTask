using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VebTechTestTask.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public RoleController(IRoleService roleService, IUserRoleService userRoleService)
        {
            _roleService = roleService;
            _userRoleService = userRoleService;
        }
        public async Task<ActionResult> GetRoleByName(string name)
        {
            try
            {
                return Ok(await _roleService.GetRoleByName(name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("roles/sort-by-name")]
        public async Task<ActionResult> SortRolesByName(int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _roleService.SortRolesByName(pageSize, pageNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("roles")]
        public async Task<ActionResult> AddUserRole(string userEmail, string roleName)
        {
            try
            {
                await _userRoleService.AddRoleToUser(userEmail, roleName);
                return Ok("Role added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
