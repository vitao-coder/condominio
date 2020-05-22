using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Condominio.Api.Models;
using Condominio.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Condominio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel loginModel)
        {
            var user = _userService.Authenticate(loginModel.Username, loginModel.Password);

            if (user == null)
                return BadRequest(new { message = "Usuário ou senha incorreta" });

            user.Password = null;

            return Ok(user);
        }
       
    }
}