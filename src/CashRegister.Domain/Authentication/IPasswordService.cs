namespace CashRegister.Domain.Authentication
{
    public interface IPasswordService
    {
        byte[] GetPbkdf2Hash(string password, byte[] salt);

        byte[] GetSalt();

        string HashPassword(string password, out string createdSalt);

        string HashPassword(string password, string base64Salt);
    }
}