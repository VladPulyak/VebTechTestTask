using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}