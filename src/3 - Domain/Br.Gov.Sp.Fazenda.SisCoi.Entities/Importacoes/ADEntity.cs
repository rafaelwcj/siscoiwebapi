using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class ADEntity : BaseImportacaoEntity
    {
        public string sAMAccountName { get; set; }
        public string whenCreated { get; set; }
        public string operatingSystem { get; set; }
        public string operatingSystemVersion { get; set; }
        public string operatingSystemServicePack { get; set; }
        public string description { get; set; }
        public string domain { get; set; }
    }
}
