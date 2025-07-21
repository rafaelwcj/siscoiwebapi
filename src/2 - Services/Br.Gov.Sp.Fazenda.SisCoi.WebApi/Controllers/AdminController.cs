using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "APP_siscoi_admin,APP_siscoi_users")]
    public class AdminController : ApiController
    {
        public AdminController(INotificationHandler<DomainNotification> notifications)
        : base(notifications)
        {
        }
        
        [HttpGet("Sobre")]
        public IActionResult About()
        {
            return Response(new List<string>() { "teste1", "teste2", "teste3" });
        }
    }
}
