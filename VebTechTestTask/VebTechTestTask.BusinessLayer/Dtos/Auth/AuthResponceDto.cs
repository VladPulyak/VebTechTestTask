using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Auth
{
    public class AuthResponceDto
    {
        public string? UserEmail { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}
