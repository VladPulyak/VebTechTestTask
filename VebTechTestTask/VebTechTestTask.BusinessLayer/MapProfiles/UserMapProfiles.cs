using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.User;
using DAL.Entities;
using BusinessLayer.Dtos.Roles;
using BusinessLayer.Dtos.Auth;

namespace BusinessLayer.MapProfiles
{
    public class UserMapProfiles : Profile
    {
        public UserMapProfiles()
        {
            CreateMap<User, CreateUserRequestDto>().ReverseMap();
            CreateMap<UpdateUserRequestDto, User>().ReverseMap();
            CreateMap<User, UpdateUserResponceDto>().ReverseMap();
            CreateMap<User, CreateUserResponceDto>().ReverseMap();
            CreateMap<User, UserRegisterRequestDto>().ReverseMap();
            CreateMap<User, GetUserByIdResponceDto>()
                 .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<RoleResponceDto>()));
        }
    }
}