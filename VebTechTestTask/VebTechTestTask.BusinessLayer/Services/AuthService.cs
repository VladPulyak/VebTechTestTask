using AutoMapper;
using BusinessLayer.Dtos.Auth;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DAL.Exceptions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IMapper mapper, IUserRepository userRepository, IHttpContextAccessor contextAccessor, IUserTokenRepository userTokenRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
            _userTokenRepository = userTokenRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<AuthResponceDto> Register(UserRegisterRequestDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            var userRole = await _roleRepository.GetByName("User");
            user.Id = Guid.NewGuid().ToString();
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
            await _userRepository.Add(user);
            await _userRoleRepository.Add(new UserRole
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = userRole.Id,
                UserId = user.Id
            });
            await _userRepository.Save();
            return await Login(new UserLoginRequestDto
            {
                Login = requestDto.Login,
                Password = requestDto.Password
            });
        }

        public async Task<AuthResponceDto> Login(UserLoginRequestDto requestDto)
        {
            var user = await _userRepository.GetByLogin(requestDto.Login);

            if (!BCrypt.Net.BCrypt.Verify(requestDto.Password, user.HashPassword))
            {
                throw new IncorrectPasswordException("Incorrect password");
            }

            var token = await CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            await SetRefreshToken(refreshToken, user);
            return new AuthResponceDto
            {
                AccessToken = token,
                RefreshToken = refreshToken.RefreshToken,
                UserEmail = user.Email
            };
        }

        public async Task<AuthResponceDto> RefreshToken(RefreshTokenRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userRepository.GetByEmail(userEmail);
            var userRefreshToken = await _userTokenRepository.GetByUserId(user.Id);
            if (user is null)
            {
                throw new ObjectNotFoundException("User with this email is not found");
            }
            if (userRefreshToken.RefreshToken != requestDto.RefreshToken)
            {
                throw new InvalidRefreshTokenException("Invalid refresh token");
            }
            else if (userRefreshToken.ExpiresDate < DateTime.UtcNow)
            {
                throw new ExpiredRefreshTokenException("Refresh token is expired");
            }
            string token = await CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken, user);
            return new AuthResponceDto
            {
                AccessToken = token,
                RefreshToken = newRefreshToken.RefreshToken,
                UserEmail = user.Email
            };
        }

        private async Task<string> CreateToken(User user)
        {
            var userWithRoles = await _userRepository.GetById(user.Id);
            var roles = userWithRoles.Roles.Select(q => q.Name).ToList();
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
            }.Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private UserToken GenerateRefreshToken()
        {
            return new UserToken()
            {
                Id = Guid.NewGuid().ToString(),
                RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresDate = DateTime.UtcNow.AddDays(1),
                CreatedDate = DateTime.UtcNow
            };
        }

        private async Task SetRefreshToken(UserToken token, User user)
        {
            var refreshToken = await _userTokenRepository.GetByUserId(user.Id);
            if (refreshToken is null)
            {
                token.UserId = user.Id;
                await _userTokenRepository.Add(token);
                await _userTokenRepository.Save();
            }
            else
            {
                refreshToken.RefreshToken = token.RefreshToken;
                refreshToken.ExpiresDate = token.ExpiresDate;
                refreshToken.CreatedDate = token.CreatedDate;
                _userTokenRepository.Update(refreshToken);
                await _userTokenRepository.Save();
            }
        }
    }
}
