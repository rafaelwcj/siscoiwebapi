using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class NetBkpAgenteEntity : BaseImportacaoEntity
    {
        public string name { get; set; }
        public string versionLabel { get; set; }
        public string lastUpdatedTime { get; set; }
        public string osDescription { get; set; }
        public string hardwareDescription { get; set; }
    }

    public class NetBkpPoliciesEntity : BaseImportacaoEntity {
        public string name { get; set; }
        public string Politica { get; set; }
        public string Client { get; set; }
        public string BackupSelection { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleType { get; set; }
    }
}
