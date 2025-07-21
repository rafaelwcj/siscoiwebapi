using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using System.Collections.Generic;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services
{
    public interface IIncidentesService
    {
        public IEnumerable<IncidentesEntity> Listar(string dtInicial, string dtFinal, string cnpj);
    }
}
