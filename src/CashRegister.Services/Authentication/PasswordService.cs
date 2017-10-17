using System;
using System.Security.Cryptography;
using CashRegister.Domain.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CashRegister.Services.Authentication
{
    public class PasswordService : IPasswordService
    {
        public byte[] GetPbkdf2Hash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashedPassword = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return hashedPassword;
        }

        public byte[] GetSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string HashPassword(string password, out string createdSalt)
        {
            var salt = GetSalt();
            createdSalt = Convert.ToBase64String(salt);
            var hash = GetPbkdf2Hash(password, salt);
            return Convert.ToBase64String(hash);
        }

        public string HashPassword(string password, string base64Salt)
        {
            var salt = Convert.FromBase64String(base64Salt);
            var hash = GetPbkdf2Hash(password, salt);
            return Convert.ToBase64String(hash);
        }
    }
}