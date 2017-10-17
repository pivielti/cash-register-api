using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using Ores.CustomerPortal.Api.Tools;
using CashRegister.Domain.Authentication;

namespace CashRegister.Api.Tools
{
    public class AuthorizeRolePolicy
    {
        public const string Administrator = "ADMIN_POLICY";
        public const string Employee = "EMPL_POLICY";

        public class Requirement : AuthorizationHandler<Requirement>, IAuthorizationRequirement
        {
            private readonly RoleType _roleToAllow;

            public Requirement(RoleType role)
            {
                _roleToAllow = role;
            }

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
            {
                if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    context.Fail();
                    return Task.FromResult(0);
                }

                var claimValue = context.User.GetRole();

                if (_roleToAllow.ToString() == claimValue)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

                return Task.FromResult(0);
            }
        }
    }
}
