using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.Roles;
using DAL.Entities;

namespace BusinessLayer.MapProfiles
{
    public class RoleMapProfiles : Profile
    {
        public RoleMapProfiles()
        {
            CreateMap<Role, GetRoleByNameResponceDto>().ReverseMap();
        }
    }
}
