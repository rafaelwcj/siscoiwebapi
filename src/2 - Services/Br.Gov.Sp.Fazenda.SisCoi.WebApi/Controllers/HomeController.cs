using Br.Gov.Sp.Fazenda.SisCoi.WebApi.Model;
using Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConfigurationRoot _configurationRoot; 

        public HomeController()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json")
                    .AddEnvironmentVariables();
            _configurationRoot = (ConfigurationRoot)builder.Build();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identities.Any(identity => identity.IsAuthenticated))
            {
                var usuario = new UsuarioModel
                {
                    Nome = string.IsNullOrEmpty(User.Claims.LastOrDefault(x => x.Type.Contains("name")).Value) ? string.Empty : User.Claims.LastOrDefault(x => x.Type.Contains("name")).Value,
                    Email = string.IsNullOrEmpty(User.Claims.FirstOrDefault(x => x.Type.Contains("emailaddress")).Value) ? string.Empty : User.Claims.FirstOrDefault(x => x.Type.Contains("emailaddress")).Value,
                    CPF = string.IsNullOrEmpty(User.Claims.FirstOrDefault(x => x.Type.Contains("CPF")).Value) ? string.Empty : User.Claims.FirstOrDefault(x => x.Type.Contains("CPF")).Value,
                    Login = string.IsNullOrEmpty(User.Claims.FirstOrDefault(x => x.Type.Contains("Login")).Value) ? string.Empty : User.Claims.FirstOrDefault(x => x.Type.Contains("Login")).Value,
                    Departamento = string.IsNullOrEmpty(User.Claims.FirstOrDefault(x => x.Type.Contains("Exercicio")).Value) ? string.Empty : User.Claims.FirstOrDefault(x => x.Type.Contains("Exercicio")).Value,
                    Role = string.IsNullOrEmpty(User.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value) ? string.Empty : User.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value,
                    Grupo = "APP_siscoi_admin;APP_siscoi_users_smon",
                };
                usuario.Token = GenerateToken(usuario.Login, usuario.Grupo);

                return View(usuario);
            }

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, WsFederationAuthenticationDefaults.AuthenticationType);
        }

        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme,
                WsFederationAuthenticationDefaults.AuthenticationType);
        }

        public IActionResult Error()
        {
            return View();
        }

        private string GenerateToken(string login, string grupo)
        {
            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.UTF8.GetBytes(_configurationRoot["AppSettings:Secreta"]);

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
