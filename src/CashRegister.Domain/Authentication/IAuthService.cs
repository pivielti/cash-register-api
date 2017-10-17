using System;

namespace CashRegister.Domain.Authentication
{
    public interface IAuthService
    {
        Tuple<bool, User> CanProvideToken(string login, string password);
    }
}