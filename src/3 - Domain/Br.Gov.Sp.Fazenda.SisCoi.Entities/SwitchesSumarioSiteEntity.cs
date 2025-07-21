using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class SwitchesSumarioSiteEntity
    {
        public string nm_site { get; set; }
        public int qt_portas_up { get; set; }
        public int qt_portas_livres { get; set; }

        public int qt_portas_ns { get; set; }       // no_sync
        public int qt_portas_desc { get; set; }     // desconectadas ou no_light
        public int qt_portas_trunk { get; set; }    // trunk ou no_module

        public int qt_portas_total { get; set; }    // totais desconsiderando Trunks
        public int qt_portas_total2 { get; set; }   // totais incluindo Trunks
    }
}
