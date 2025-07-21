using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class VcStgDsEntity
    {
        public string nm_storage { get; set; }
        public string ds_site { get; set; }
        public long qt_devs_raw { get; set; }
        public long qt_devs_ds { get; set; }
        public long qt_devs_vmdk { get; set; }
        public long qt_devs { get { return qt_devs_raw + qt_devs_ds; } }

        public decimal qt_capacityMB_raw { get; set; }
        public decimal qt_capacityGB_raw { get { return qt_capacityMB_raw / 1024; } }
        public decimal qt_capacityTB_raw { get { return qt_capacityGB_raw / 1024; } }

        public decimal qt_capacityMB_vmdk { get; set; }
        public decimal qt_capacityGB_vmdk { get { return qt_capacityMB_vmdk / 1024; } }
        public decimal qt_capacityTB_vmdk { get { return qt_capacityGB_vmdk / 1024; } }

        public decimal qt_capacityMB_ds { get; set; }
        public decimal qt_capacityGB_ds { get { return qt_capacityMB_ds / 1024; } }
        public decimal qt_capacityTB_ds { get { return qt_capacityGB_ds / 1024; } }

        public decimal qt_capacityGB { get { return qt_capacityGB_raw + qt_capacityGB_ds; } }
        public decimal qt_capacityTB { get { return qt_capacityTB_raw + qt_capacityTB_ds; } }
    }
}
