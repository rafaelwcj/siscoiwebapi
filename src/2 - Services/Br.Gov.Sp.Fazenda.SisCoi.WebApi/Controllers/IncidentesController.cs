using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class IncidentesController : ApiController
    {
        IIncidentesService _incidentesService;
        public IncidentesController(IIncidentesService incidentesService,
                                 INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _incidentesService = incidentesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response("ok");
        }

        [HttpGet("Listar")]
        public IActionResult Listar(string dtInicial, string dtFinal, string cnpj)
        {
            return Response(_incidentesService.Listar(dtInicial, dtFinal, cnpj));
        }
    }
}
