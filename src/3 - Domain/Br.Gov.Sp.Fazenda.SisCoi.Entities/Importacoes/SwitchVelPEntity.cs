using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SwitchVelPEntity : BaseImportacaoEntity
    {
        public string nm_switch { get; set; }
        public string cd_ip { get; set; }
        public int id_mod { get; set; }
        public int qt_ports { get; set; }
        public string tp_mod { get; set; }
        public string ds_model { get; set; }
        public string ds_status { get; set; }
    }
}
