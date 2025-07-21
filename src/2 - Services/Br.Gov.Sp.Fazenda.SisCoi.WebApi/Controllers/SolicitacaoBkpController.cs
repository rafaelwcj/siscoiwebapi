using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitacaoBkpController : ApiController
    {
        ISolicitacaoBkpService _solicitacaoBkpService;

        public SolicitacaoBkpController(ISolicitacaoBkpService solicitacaoBkpService,
                                        INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _solicitacaoBkpService = solicitacaoBkpService;
        }

        [HttpGet("Listar")]
        public IActionResult Get(string servidor, int depto)
        {
            return Response(_solicitacaoBkpService.Listar(servidor, depto));
        }
    }
}
