using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class AlocacaoEntity : BaseImportacaoEntity
    {
        public string machineName { get; set; }
        public string instanceName { get; set; }
        public string dataBaseName { get; set; }
        public string alocadoMB { get; set; }
    }
}
