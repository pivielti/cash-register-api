namespace CashRegister.Domain.Authentication
{
    public interface IUserRepository
    {
        User GetUser(string login);
    }
}