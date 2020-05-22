using System;
using System.Threading.Tasks;
using Domain;
using Repository;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user ==  null)
                return Task.FromResult(NotFound(new { message = "Usuário ou senha inválidos" }));

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            
            var result = new
            {
                user,
                token
            };
            return await Task.FromResult(result);
       }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);
    }
}