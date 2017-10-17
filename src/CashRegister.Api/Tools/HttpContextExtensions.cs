using Microsoft.AspNetCore.Http;

namespace CashRegister.Api.Tools
{
    public static class HttpContextExtensions
    {
        public static void AddAuthorizationHeader(this HttpContext context, string token)
        {
            context.Response.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
