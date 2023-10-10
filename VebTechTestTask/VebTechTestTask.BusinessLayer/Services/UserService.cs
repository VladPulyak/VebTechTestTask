using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.Pagination;
using BusinessLayer.Dtos.User;
using BusinessLayer.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Dtos.Roles;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<PagedResult<List<User>>> GetUsersByAge(int minimalAge, int maximalAge, int pageSize, int pageNumber)
        {
            var users = await _userRepository.GetByAge(minimalAge, maximalAge);
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<User>>
            {
                Data = pagedUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (users.Count / pageSize) + 1
            };
        }

        public async Task<PagedResult<List<User>>> GetUsersByName(string name, int pageSize, int pageNumber)
        {
            var users = await _userRepository.GetByName(name);
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<User>>
            {
                Data = pagedUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (users.Count / pageSize) + 1
            };
        }

        public async Task<PagedResult<List<User>>> SortUsersByAge(int pageSize, int pageNumber)
        {
            var users = await _userRepository.SortByAge();
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<User>>
            {
                Data = pagedUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (users.Count / pageSize) + 1
            };
        }

        public async Task<PagedResult<List<User>>> SortUsersByName(int pageSize, int pageNumber)
        {
            var users = await _userRepository.SortByName();
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<User>>
            {
                Data = pagedUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (users.Count / pageSize) + 1
            };
        }

        public async Task<GetUserByIdResponceDto> GetById(string id)
        {
            var user = await _userRepository.GetById(id);
            var userRoles = await _userRoleRepository.GetByUserId(user.Id);
            var responceDto = _mapper.Map<GetUserByIdResponceDto>(user);
            responceDto.Roles = userRoles.Select(q => new RoleResponceDto
            {
                Name = q.Role.Name
            }).ToList();
            return responceDto;
        }

        public async Task<CreateUserResponceDto> CreateUser(CreateUserRequestDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            user.Id = Guid.NewGuid().ToString();
            var addedUser = await _userRepository.Add(user);
            await _userRepository.Save();
            return _mapper.Map<CreateUserResponceDto>(addedUser);
        }

        public async Task<UpdateUserResponceDto> UpdateUser(UpdateUserRequestDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            var updatedUser = _userRepository.Update(user);
            await _userRepository.Save();
            return _mapper.Map<UpdateUserResponceDto>(updatedUser);
        }

        public async Task DeleteUser(string id)
        {
            await _userRepository.Delete(id);
            await _userRepository.Save();
        }

        public async Task<PagedResult<List<User>>> GetUsers(int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAll().ToListAsync();
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<List<User>>
            {
                Data = pagedUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (users.Count / pageSize) + 1
            };
        }

        public async Task<bool> IsExistsUser(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user is not null;
        }
    }
}
