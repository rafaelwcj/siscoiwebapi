using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class VcTotalEntity
    {
        public string nm_site { get; set; }
        public long qt_clusters { get; set; }
        public long qt_hosts { get; set; }
        public long qt_vm { get; set; }

        public long qt_devs_raw { get; set; }
        public decimal qt_capacityMb_raw { get; set; }
        public decimal qt_capacityTb_raw { get { return qt_capacityMb_raw / 1048576; } }

        public long qt_devs_ds { get; set; }
        public decimal qt_capacityMb_ds { get; set; }
        public decimal qt_capacityTb_ds { get { return qt_capacityMb_ds / 1048576; } }

        public long qt_devs_total { get { return qt_devs_raw + qt_devs_ds; } }
        public decimal qt_capacityMb_total { get { return qt_capacityMb_ds + qt_capacityMb_raw; } }
        public decimal qt_capacityTb_total { get { return qt_capacityMb_total / 1048576; } }        
    }
}
