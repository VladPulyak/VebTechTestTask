using BusinessLayer.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponceDto> Register(UserRegisterRequestDto requestDto);

        Task<AuthResponceDto> Login(UserLoginRequestDto requestDto);

        Task<AuthResponceDto> RefreshToken(RefreshTokenRequestDto requestDto);


    }
}
