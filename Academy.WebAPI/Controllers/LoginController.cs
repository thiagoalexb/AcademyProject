using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.WebAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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

            if (hasUser)
            {
                return new ManageToken().GetLoginObject(tokenConfigurations, signingConfigurations, user);
            }

            return new
            {
                authenticated = false,
                message = "Usuário não encontrado"
            };
        }
    }
}