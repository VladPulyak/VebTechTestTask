using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserToken
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime ExpiresDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public User? User { get; set; }
    }
}
