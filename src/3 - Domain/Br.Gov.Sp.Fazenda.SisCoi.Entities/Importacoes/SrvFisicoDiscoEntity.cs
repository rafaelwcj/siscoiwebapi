using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SrvFisicoDiscoEntity : BaseImportacaoEntity
    {
        public string nm_servidor { get; set; }
        public string nm_storage { get; set; }
        public string ds_os { get; set; }
        public string ds_label { get; set; }
        public string cd_wwn { get; set; }
        public int? cd_scsi_lun { get; set; }

        public decimal qt_total_gb { get; set; }
        public decimal qt_aloc_gb { get; set; }
        public decimal qt_livre_gb { get; set; }
    }
}
