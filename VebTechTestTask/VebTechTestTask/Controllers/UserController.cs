using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dtos.User;
using BusinessLayer.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace VebTechTestTask.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users/fiter-by-name")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> GetUsersPageByName(string name, int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _userService.GetUsersByName(name, pageSize, pageNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/filter-by-age")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> GetUsersPageByAge(int minimalAge, int maximalAge, int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _userService.GetUsersByAge(minimalAge, maximalAge, pageSize, pageNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/sort-by-age")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> SortUsersPageByAge(int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _userService.SortUsersByAge(pageSize, pageNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/sort-by-name")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> SortUsersPageByName(int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _userService.SortUsersByName(pageSize, pageNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("users")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> UpdateUser(UpdateUserRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _userService.UpdateUser(requestDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult> CreateUser(CreateUserRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _userService.IsExistsUser(requestDto.Email))
            {
                try
                {
                    return Ok(await _userService.CreateUser(requestDto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("User with this email already exists");
            }
        }

        [HttpGet("users/by-id")]
        [Authorize(Roles = "Support")]
        public async Task<ActionResult> GetUserById(string id)
        {
            try
            {
                return Ok(await _userService.GetById(id));
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> GetUsers(int pageSize, int pageNumber)
        {
            try
            {
                return Ok(await _userService.GetUsers(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
