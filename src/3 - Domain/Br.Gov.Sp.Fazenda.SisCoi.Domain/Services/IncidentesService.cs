using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Validations;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using System.Collections.Generic;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Services
{
    public class IncidentesService : IIncidentesService
    {
        IIncidentesRepository _incidentesRepository;
        readonly IMediatorHandler _mediator;

        public IncidentesService(IIncidentesRepository incidentesRepository,
                                 IMediatorHandler mediator)
        {
            _mediator = mediator;
            _incidentesRepository = incidentesRepository;
        }

        public IEnumerable<IncidentesEntity> Listar(string dtInicial, string dtFinal, string cnpj)
        {
            var valid = true;
            var msgData = Datas.IsValid(dtInicial, dtFinal);
            var msgCnpj = Cnpj.IsValid(cnpj);

            if (!string.IsNullOrEmpty(msgData))
            {
                _mediator.RaiseEvent(new DomainNotification(string.Empty, msgData));
                valid = false;
            }
            if (!string.IsNullOrEmpty(msgCnpj))
            {
                _mediator.RaiseEvent(new DomainNotification(string.Empty, msgCnpj));
                valid = false;
            }

            if (!valid)
                return null;

            return _incidentesRepository.Listar(dtInicial, dtFinal, cnpj);
        }
    }
}
