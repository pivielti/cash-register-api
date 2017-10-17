using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using CashRegister.Api.Tools;
using CashRegister.Domain.Authentication;

namespace CashRegister.Api
{
    public static class Startup_Auth
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfigurationRoot conf, IHostingEnvironment env)
        {
            var secretKey = conf.GetSection("TokenAuthentication:SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var issuer = conf.GetSection("TokenAuthentication:Issuer").Value;
            var audience = conf.GetSection("TokenAuthentication:Audience").Value;
            var expiration = int.Parse(conf.GetSection("TokenAuthentication:Expiration").Value);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = !env.IsDevelopment(),

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,

                //LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => {
                //    return true;
                //}
            };

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Configuration = new OpenIdConnectConfiguration();
                    options.Audience = audience;
                    options.Authority = issuer;
                    options.ClaimsIssuer = issuer;
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizeRolePolicy.Administrator, policy =>
                    policy.Requirements.Add(new AuthorizeRolePolicy.Requirement(RoleType.Administrator))
                );

                options.AddPolicy(AuthorizeRolePolicy.Employee, policy =>
                    policy.Requirements.Add(new AuthorizeRolePolicy.Requirement(RoleType.Employee))
                );
            });
        }
    }
}
