using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CashRegister.Domain.Authentication
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("/^[a-z0-9_-]{3,16}$/")]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string HashSalt { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<Role> GetRoles()
        {
            return UserRoles.Select(x => x.Role).ToList();
        }
    }
}
