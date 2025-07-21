using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SwitchDescPortaEntity : BaseImportacaoEntity
    {
        public string cd_ip { get; set; }
        public string nm_porta { get; set; }
        public string ds_porta { get; set; }
    }
}
