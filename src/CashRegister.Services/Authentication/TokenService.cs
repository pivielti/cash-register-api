using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CashRegister.Domain.Authentication;
using CashRegister.Services.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CashRegister.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly AuthenticationSettings _options;

        public TokenService(IOptions<AuthenticationSettings> options)
        {
            _options = options.Value;
        }

        public string CreateToken(User user)
        {
            var securityToken = GetNewToken(user.Login);
            return EncodeToken(securityToken);
        }

        public string UpdateToken(string token)
        {
            var decodedToken = DecodeToken(token);

            var now = DateTime.UtcNow;

            var expirationLength = TimeSpan.FromMinutes(_options.Expiration);

            var expiration = decodedToken.ValidTo;
            if (expiration.Subtract(now) <= expirationLength)
                expiration = expiration.Add(expirationLength);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: decodedToken.Claims,
                notBefore: decodedToken.ValidFrom,
                expires: expiration,
                signingCredentials: GetCredentials());

            return EncodeToken(jwt);
        }


        private string EncodeToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken DecodeToken(string token)
        {
            var prefix = "Bearer ";
            if (token.StartsWith(prefix))
            {
                token = token.Substring(prefix.Length);
            }
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }

        private JwtSecurityToken GetNewToken(string subjectClaimValue, params Claim[] customClaims)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                // Subject - https://tools.ietf.org/html/rfc7519#section-4.1.2
                new Claim(JwtRegisteredClaimNames.Sub, subjectClaimValue),
                // JWT ID - https://tools.ietf.org/html/rfc7519#section-4.1.7
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Issued At - https://tools.ietf.org/html/rfc7519#section-4.1.6
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            if (customClaims != null)
            {
                claims = claims.Concat(customClaims).ToArray();
            }

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_options.Expiration)),
                signingCredentials: GetCredentials());

            return jwt;
        }

        private SigningCredentials GetCredentials()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}