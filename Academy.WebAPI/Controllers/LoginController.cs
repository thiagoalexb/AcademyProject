using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.Domain.Services.Interfaces;
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
            bool hasUser = false;

            var user = _userAppService.GetByEmailAndPassword(loginViewModel.Email, loginViewModel.Password);

            hasUser = user != null;

            if(!hasUser) return new { authenticated = false, message = "Usuário não encontrado" };

            if(!user.IsVerified) return new { authenticated = false, message = "Confirme seu e-mail" };
            
            if (hasUser) return new ManageToken().GetLoginObject(tokenConfigurations, signingConfigurations, user);

            return new { authenticated = false, message = "Algum erro ocorreu. Tente novamente!" };
        }
    }
}