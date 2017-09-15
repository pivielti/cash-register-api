using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CashRegister.Domain.Authentication
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<User> GetUsers()
        {
            return UserRoles.Select(x => x.User).ToList();
        }
    }
}
