
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Api
{
    public static class Startup_Options
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfigurationRoot conf)
        {
            // services.Configure<AuthenticationSettings>(conf.GetSection("TokenAuthentication"));

            return services;
        }
    }
}
