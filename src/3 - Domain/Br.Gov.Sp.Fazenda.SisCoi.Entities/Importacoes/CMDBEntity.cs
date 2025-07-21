using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class CMDBEntity: BaseImportacaoEntity
    {
        public string IC { get; set; }
        public string Grupo { get; set; }
        public string Dono { get; set; }
        public string Localidade { get; set; }
        public string Tipo { get; set; }
        public string SistemaOperacional { get; set; }
        public string IP { get; set; }
    }
}
