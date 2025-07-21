using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Util.LDAP;
using Br.Gov.Sp.Fazenda.SisCoi.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ApiController
    {
        private static IConfiguration _configuration;
        IMediatorHandler _mediator;

        public LoginController(IConfiguration configuration,
                                IMediatorHandler mediator,
                                INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel loginModel)
        {
            string token = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(loginModel.Login) && !string.IsNullOrEmpty(loginModel.Senha))
                {
                    var autenticado = LoginService.Login(loginModel.Senha);

                    if (autenticado)
                    {
                        token = GenerateToken(loginModel.Login, loginModel.Grupo);
                    }
                    else
                    {
                        _mediator.RaiseEvent(new DomainNotification("Autenticação", "Usuário ou Senha não conferem."));
                    }
                }
                else
                {
                    _mediator.RaiseEvent(new DomainNotification("Autenticação", "Usuário e Senha são obrigatórios."));
                }
            }
            catch (Exception e)
            {
                _mediator.RaiseEvent(new DomainNotification("Autenticação", e.Message));
            }
            return Response(token);
        }

        private static string GenerateToken(string login, string grupo)
        {
            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.UTF8.GetBytes(_configuration["AppSettings:Secreta"]);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var listClaims = new List<Claim>();

            listClaims.Add(new Claim(ClaimTypes.NameIdentifier, login));

            if (!string.IsNullOrEmpty(grupo))
            {
                var roules = grupo.Split(";".ToCharArray()).ToList();

                foreach (var roule in roules)
                {
                    listClaims.Add(new Claim(ClaimTypes.Role, roule));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = new ClaimsIdentity(listClaims)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }

}
