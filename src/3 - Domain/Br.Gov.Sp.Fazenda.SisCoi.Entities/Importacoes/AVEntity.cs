using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class AVEntity : BaseImportacaoEntity
    {
        public string SystemName { get; set; }
        public string ProductVersion { get; set; }
        public double DATVersion { get; set; }
    }
}
