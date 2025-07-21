using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class VcDskEntity
    {
        public string nm_vm { get; set; }
        public string nm_storage { get; set; }
        public string nm_site { get; set; }
        public string nm_cluster { get; set; }
        public string ds_aplicacao { get; set; }
        public string nm_disco { get; set; }
        public string RawLunId { get; set; }
        public string Path { get; set; }
        public string ds_raw { get; set; }
        public string nm_datastore { get; set; }
        public string cd_lun { get; set; }
        public string nm_host { get; set; }
        public string nm_os { get; set; }
        public decimal qt_capacityMb { get; set; }
        public decimal qt_capacityGb { get { return qt_capacityMb / 1024; } }
        public decimal qt_capacityTb { get { return qt_capacityMb / 1048576; } }
    }
}

