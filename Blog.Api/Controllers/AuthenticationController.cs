using Blog.Api.Models;
using Blog.Api.Repositories.Interfaces;
using Blog.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("v1/authenticaton")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
       private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model)
        {
            if (model == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = _jwtTokenService.GenerateToken(model);
            
            return new
            {
                user = model.UserName,
                token = token
            };
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);
    }
}
