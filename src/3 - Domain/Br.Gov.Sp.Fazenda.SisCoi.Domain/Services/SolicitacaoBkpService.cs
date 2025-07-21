using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Services
{
    public class SolicitacaoBkpService : ISolicitacaoBkpService
    {
        ISolicitacaoBkpRepository _solicitacaoBkpRepository;
        readonly IMediatorHandler _mediator;

        public SolicitacaoBkpService(ISolicitacaoBkpRepository solicitacaoBkpRepository,
                                     IMediatorHandler mediator)
        {
            _solicitacaoBkpRepository = solicitacaoBkpRepository;
            _mediator = mediator;
        }

        public IEnumerable<SolicitacaoBkpEntity> Listar(string nomeServidor, int cdDepto)
        {
            var result = _solicitacaoBkpRepository.Listar(nomeServidor, cdDepto);

            if(result.Count() == 0)
            {
                _mediator.RaiseEvent(new DomainNotification(string.Empty, "Nenhum registro encontrado."));
                return null;
            }

            return result;
        }
    }
}
