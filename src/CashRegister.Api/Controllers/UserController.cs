using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Api.Controllers
{
    [Authorize]
    [Route("users")]
    public class UserController : Controller
    {
        public UserController()
        {
        }

        [HttpGet("")]
        public IActionResult GetList()
        {
            return Ok();
        }

        [HttpPost("")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPatch("")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete("")]
        public IActionResult Delete()
        {
            return Ok();
        }

        [HttpPatch("password")]
        public IActionResult ChangePassword()
        {
            return Ok();
        }
    }
}