using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "APP_siscoi_admin,APP_siscoi_users")]
    public class CmdbServicesController : ApiController
    {
        ICmdbServicesService _cmdbService;
        public CmdbServicesController(INotificationHandler<DomainNotification> notifications, ICmdbServicesService cmdbService) : base(notifications)
        {
            _cmdbService = cmdbService;
        }

        [HttpGet]
        public IActionResult Get(string dtInicial, string dtFinal)
        {
            return Response(_cmdbService.ListarPorDatas(dtInicial, dtFinal));
        }

        [HttpGet("ListarAtivosPorPeriodo")]
        public IActionResult ListarAtivosPorDatas(string dtInicial, string dtFinal, string cnpj = null)
        {
            return Response(_cmdbService.ListarAtivosPorDatas(dtInicial, dtFinal, cnpj));
        }
    }
}
