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
    public class ServidoresController : ApiController
    {
        public ServidoresController(INotificationHandler<DomainNotification> notifications)
        : base(notifications)
        {
        }

        [HttpGet("Listar")]
        public IActionResult listar()
        {
            return Response(new List<string>() { "Patrimonio", "Almoxerifado", "Contas à pagar" });
        }
    }
}
