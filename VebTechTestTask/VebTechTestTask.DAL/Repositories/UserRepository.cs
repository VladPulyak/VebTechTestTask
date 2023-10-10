using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(VebTechDbContext context) : base(context)
        {
        }

        public override async Task<User> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidArgumentException("Invalid argument");
            }

            var user = await _set
                .Where(q => q.Id == id)
                .Include(q => q.UserRoles)
                .Include(q => q.Roles)
                .SingleOrDefaultAsync();

            if (user is null)
            {
                throw new ObjectNotFoundException("User with this id is not found");
            }

            return user;

        }

        public async Task<List<User>> GetByAge(int minimalAge, int maximalAge)
        {
            return await _set
                .Where(q => q.Age >= minimalAge && q.Age <= maximalAge)
                .ToListAsync();
        }

        public async Task<List<User>> GetByName(string name)
        {
            return await _set
                .Where(q => q.Name == name)
                .ToListAsync();
        }

        public async Task<List<User>> SortByAge()
        {
            return await _set
                .OrderBy(q => q.Age)
                .ToListAsync();
        }

        public async Task<List<User>> SortByName()
        {
            return await _set
                .OrderBy(q => q.Name)
                .ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _set
                .Where(q => q.Email == email)
                .Include(q => q.UserRoles)
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _set
                .SingleAsync(q => q.Login == login);
        }
    }
}
