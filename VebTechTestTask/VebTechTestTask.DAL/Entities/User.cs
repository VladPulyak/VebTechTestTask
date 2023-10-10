using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Age must be positive")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format does not match")]
        public string? Email { get; set; }

        public string? Login { get; set; }

        public string? HashPassword { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }

        public ICollection<Role>? Roles { get; set; }

        public UserToken? Token { get; set; }
    }
}
