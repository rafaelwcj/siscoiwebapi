using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services
{
    public interface ISolicitacaoBkpService
    {
        public IEnumerable<SolicitacaoBkpEntity> Listar(string nomeServidor, int cdDepto);
    }
}
