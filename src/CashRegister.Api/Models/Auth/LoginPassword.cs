using System.ComponentModel.DataAnnotations;

namespace CashRegister.Api.Models.Auth
{
    public class LoginPassword
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}