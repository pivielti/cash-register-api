using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashRegister.Api.Models.Auth;
using CashRegister.Api.Tools;
using CashRegister.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Api.Controllers
{
    [Authorize]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly HttpContext _httpContext;

        public AuthController(
            IAuthService authService,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _tokenService = tokenService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginPassword model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var auth = _authService.CanProvideToken(model.Login, model.Password);
            if (!auth.Item1)
            {
                ModelState.AddModelError(nameof(model.Login), "Wrong login or password");
                return BadRequest(ModelState);
            }

            var token = _tokenService.CreateToken(auth.Item2);

            _httpContext.AddAuthorizationHeader(token);

            return Ok($"Bearer {token}");
        }

        [HttpPost("refresh")]
        public IActionResult TokenRefresh()
        {
            string token = _httpContext.Request.Headers["Authorization"];

            token = _tokenService.UpdateToken(token);

            _httpContext.AddAuthorizationHeader(token);

            return Ok($"Bearer {token}");
        }
    }
}
