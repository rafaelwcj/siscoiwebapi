using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using System.Collections.Generic;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Services
{
    public interface ICmdbServicesService
    {
        public IEnumerable<CMDBServicesEntity> ListarAtivosPorDatas(string dtInicial, string dtFinal, string cnpj);
        public IEnumerable<CMDBServicesEntity> ListarPorDatas(string dtInicial, string dtFinal);
    }
}
