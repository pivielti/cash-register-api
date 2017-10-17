using System;

namespace CashRegister.Services.Settings
{
    public class AuthenticationSettings
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expiration { get; set; }
    }
}
