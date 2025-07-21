using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class SrvFisicoTotaisStorageEntity : BaseImportacaoEntity
    {
        public string nm_servidor { get; set; }
        public string nm_storage { get; set; }
        public int qt_discos { get; set; }
        public decimal qt_total_gb { get; set; }
        public decimal qt_total_tb { get; set; }
    }
}
