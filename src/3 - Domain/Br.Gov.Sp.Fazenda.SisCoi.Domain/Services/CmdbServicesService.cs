using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Bus;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Notifications;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Validations;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services;
using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using System.Collections.Generic;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Services
{
    public class CmdbServicesService : ICmdbServicesService
    {
        ICmdbServicesRepository _cmdbServicesRepository;
        readonly IMediatorHandler _mediator;

        public CmdbServicesService(ICmdbServicesRepository cmdbServicesRepository,
                                   IMediatorHandler mediator)
        {
            _mediator = mediator;
            _cmdbServicesRepository = cmdbServicesRepository;
        }

        public IEnumerable<CMDBServicesEntity> ListarAtivosPorDatas(string dtInicial, string dtFinal, string cnpj)
        {
            var msg = Datas.IsValid(dtInicial, dtFinal);

            if (!string.IsNullOrEmpty(msg))
            {
                _mediator.RaiseEvent(new DomainNotification(string.Empty, msg));
                return null;
            }

            if (!string.IsNullOrEmpty(cnpj))
            {
                var msgCnpj = Cnpj.IsValid(cnpj);

                if(!string.IsNullOrEmpty(msgCnpj))
                {
                    _mediator.RaiseEvent(new DomainNotification(string.Empty, msgCnpj));
                    return null;
                }
            }

            return _cmdbServicesRepository.ListarAtivosPorDatas(dtInicial, dtFinal, cnpj);
        }

        public IEnumerable<CMDBServicesEntity> ListarPorDatas(string dtInicial, string dtFinal)
        {
            var msg = Datas.IsValid(dtInicial, dtFinal);

            if (!string.IsNullOrEmpty(msg))
            {
                _mediator.RaiseEvent(new DomainNotification(string.Empty, msg));
                return null;
            }

            var result = _cmdbServicesRepository.ListarPorDatas(dtInicial, dtFinal);

            return result;
        }
    }
}
