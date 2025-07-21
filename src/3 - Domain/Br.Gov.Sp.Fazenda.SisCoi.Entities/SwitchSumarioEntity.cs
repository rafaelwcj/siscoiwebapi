using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class SwitchSumarioEntity
    {
        public string nm_switch { get; set; }
        public string cd_ip { get; set; }
        public string nm_site { get; set; }
        public string ds_local { get; set; }

        public int qt_portas_up { get; set; }
        public int qt_portas_livres { get; set; }

        public int qt_portas_ns { get; set; }       // no_sync
        public int qt_portas_desc { get; set; }     // desconectadas ou no_light
        public int qt_portas_trunk { get; set; }    // trunk ou no_module

        public int qt_portas_total { get; set; }    // total sem trunks
        public int qt_portas_total2 { get; set; }   // total com trunks
    }
}
