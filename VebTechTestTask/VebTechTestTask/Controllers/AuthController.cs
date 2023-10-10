using BusinessLayer.Dtos.Auth;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace VebTechTestTask.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("auth/register")]
        public async Task<ActionResult> Register(UserRegisterRequestDto requestDto)
        {
            try
            {
                var responceDto = await _authService.Register(requestDto);
                _logger.LogInformation("auth/register {@responceDto}", responceDto);
                return Ok(responceDto);
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult> Login(UserLoginRequestDto requestDto)
        {
            try
            {
                var responceDto = await _authService.Login(requestDto);
                _logger.LogInformation("auth/login {@responceDto}", responceDto);
                return Ok(responceDto);
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("auth/refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequestDto requestDto)
        {
            try
            {
                var responceDto = await _authService.RefreshToken(requestDto);
                _logger.LogInformation("auth/refresh-token {@responceDto}", responceDto);
                return Ok(responceDto);
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
    }
}
