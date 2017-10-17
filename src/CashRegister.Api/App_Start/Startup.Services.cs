
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Api
{
    public static class Startup_Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddTransient<IConnectionServiceProxy, ConnectionServiceProxy>();
            
            return services;
        }
    }
}