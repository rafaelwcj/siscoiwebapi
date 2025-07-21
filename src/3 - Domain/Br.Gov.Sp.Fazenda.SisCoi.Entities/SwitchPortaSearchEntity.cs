using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class SwitchPortaSearchEntity 
    {
        public string nm_switch { get; set; }
        public string tp_switch { get; set; }
        public string cd_ip { get; set; }
        public string nm_porta { get; set; }
        public string ds_porta { get; set; }
        public string ds_local { get; set; }

        public int? id_vsan { get; set; }
        public string ds_portmode { get; set; }
        public string ds_trunkmode { get; set; }
        public string ds_status { get; set; }
        public string ds_sfp { get; set; }
        public string ds_opermode { get; set; }
        public string qt_speed { get; set; }
        public string id_portchannel { get; set; }
    }
}
