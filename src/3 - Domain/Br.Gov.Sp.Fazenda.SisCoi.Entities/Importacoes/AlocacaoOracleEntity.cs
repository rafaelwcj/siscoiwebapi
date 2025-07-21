using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class AlocacaoOracleEntity : BaseImportacaoEntity
    {
        public string cluster { get; set; }
        public string instanceName { get; set; }
        public string owner { get; set; }
        public string sizemb { get; set; }
    }
}
