using System;
using CashRegister.Domain.Authentication;

namespace CashRegister.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public AuthService(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public Tuple<bool, User> CanProvideToken(string login, string password)
        {
            var user = _userRepository.GetUser(login);
            if (user == null)
                return new Tuple<bool, User>(false, null);
            var hash = _passwordService.HashPassword(password, user.HashSalt);
            return new Tuple<bool, User>(user.PasswordHash == hash, user);
        }
    }
}