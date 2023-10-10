using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.Roles;
using DAL.Entities;

namespace BusinessLayer.Dtos.User
{
    public class GetUserByIdResponceDto
    {
        public string? Name { get; set; }

        public int? Age { get; set; }

        public string? Email { get; set; }

        public List<RoleResponceDto>? Roles { get; set; }
    }
}
