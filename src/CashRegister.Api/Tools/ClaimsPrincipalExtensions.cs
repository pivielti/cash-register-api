using System.Linq;
using System.Security.Claims;

namespace Ores.CustomerPortal.Api.Tools
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.Claims.First(x => x.Type == ClaimTypes.Role).Value;
        }
    }
}