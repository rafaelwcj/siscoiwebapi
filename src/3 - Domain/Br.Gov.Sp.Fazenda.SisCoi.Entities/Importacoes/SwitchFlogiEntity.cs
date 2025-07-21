using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SwitchFlogiEntity : BaseImportacaoEntity
    {
        public string nm_switch { get; set; }
        public string cd_ip { get; set; }

        public string nm_porta { get; set; }
        public int nr_porta { get; set; }
        public int id_vsan { get; set; }

        public string cd_fcid { get; set; }
        public string cd_portname { get; set; }
        public string cd_nodename { get; set; }
    }
}
