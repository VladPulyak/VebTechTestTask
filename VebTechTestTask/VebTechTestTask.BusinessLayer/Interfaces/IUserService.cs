using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos.Pagination;
using BusinessLayer.Dtos.User;
using DAL.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<List<User>>> GetUsersByName(string name, int pageSize, int pageNumber);

        Task<PagedResult<List<User>>> GetUsersByAge(int minimalAge, int maximalAge, int pageSize, int pageNumber);

        Task<PagedResult<List<User>>> SortUsersByName(int pageSize, int pageNumber);

        Task<PagedResult<List<User>>> SortUsersByAge(int pageSize, int pageNumber);

        Task<GetUserByIdResponceDto> GetById(string id);

        Task<CreateUserResponceDto> CreateUser(CreateUserRequestDto requestDto);

        Task<UpdateUserResponceDto> UpdateUser(UpdateUserRequestDto requestDto);

        Task DeleteUser(string id);

        Task<PagedResult<List<User>>> GetUsers(int pageNumber, int pageSize);

        Task<bool> IsExistsUser(string email);
    }
}
