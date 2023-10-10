using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetByName(string name);

        Task<User> GetByEmail(string email);

        Task<List<User>> GetByAge(int minimalAge, int maximalAge);

        Task<List<User>> SortByName();

        Task<List<User>> SortByAge();

        Task<User> GetByLogin(string login);
    }
}
