namespace CashRegister.Domain.Authentication
{
    public interface ITokenService
    {
        string CreateToken(User user);

        string UpdateToken(string token);
    }
}
