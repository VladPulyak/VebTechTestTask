using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(VebTechDbContext context) : base(context)
        {

        }

        public async Task<UserToken> GetByUserId(string userId)
        {
            return await _set
                .SingleOrDefaultAsync(q => q.UserId == userId);
        }
    }
}
