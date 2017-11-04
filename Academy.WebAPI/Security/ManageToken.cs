using Academy.Application.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Academy.WebAPI.Security
{
    public class ManageToken
    {
        public object GetLoginObject(TokenConfigurations tokenConfigurations, 
                                        SigningConfigurations signingConfigurations, 
                                        UserViewModel userViewModel)
        {
            var dates = GetDates(tokenConfigurations);
            return new
            {
                authenticated = true,
                created = dates.dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dates.dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = GenerateSecurityToken(tokenConfigurations, signingConfigurations, userViewModel, dates),
                message = "OK"
            };
        }

        public ClaimsIdentity GenerateClaimsIdentity(UserViewModel userViewModel)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userViewModel.UserId.ToString(), "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userViewModel.UserId.ToString())
                }
            );

            return identity;
        }

        public (DateTime dataCriacao, DateTime dataExpiracao) GetDates(TokenConfigurations tokenConfigurations)
        {
            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            return (dataCriacao, dataExpiracao);
        }

        public string GenerateSecurityToken(TokenConfigurations tokenConfigurations, 
                                            SigningConfigurations signingConfigurations, 
                                            UserViewModel userViewModel,
                                            (DateTime dataCriacao, DateTime dataExpiracao) dates)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = GenerateClaimsIdentity(userViewModel),
                NotBefore = dates.dataCriacao,
                Expires = dates.dataExpiracao
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
