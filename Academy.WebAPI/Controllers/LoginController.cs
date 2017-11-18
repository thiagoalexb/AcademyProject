using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.Domain.Services.Interfaces;
using Academy.Domain.Services.Utils;
using Academy.WebAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Academy.WebAPI.Controllers
{
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly IUserAppService _userAppService;
        

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public object Login(
            [FromBody] LoginViewModel loginViewModel,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            var user = _userAppService.GetByEmail(loginViewModel.Email);

            if(user == null) return new { authenticated = false, message = "Usuário não encontrado" };

            if (!user.IsVerified) return new { authenticated = false, message = "Confirme seu e-mail" };

            else if (user.Password != Utils.EncryptPassword(loginViewModel.Password)) return new { authenticated = false, message = "Usuário ou senha incorretos!" };

            else
            {
                var login = new ManageToken().GetLoginObject(tokenConfigurations, signingConfigurations, user);
                if(login == null) return new { authenticated = false, message = "Ocorreu algum erro, tente novamente." };
                return login;
            }
             
        }
    }
}