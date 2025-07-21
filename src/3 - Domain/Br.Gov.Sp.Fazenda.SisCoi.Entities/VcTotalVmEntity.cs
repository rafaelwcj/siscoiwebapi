using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class VcTotalVmEntity
    {
        public string nm_vm { get; set; }
        public string nm_cluster { get; set; }
        public string nm_host { get; set; }
        public string ds_aplicacao { get; set; }
        public int qt_disks { get; set; }
        public decimal qt_capacityMb { get; set; }
        public decimal qt_capacityGb { get { return qt_capacityMb / 1024; } }
        public decimal qt_capacityTb { get { return qt_capacityMb / 1048576; } }
    }
}
