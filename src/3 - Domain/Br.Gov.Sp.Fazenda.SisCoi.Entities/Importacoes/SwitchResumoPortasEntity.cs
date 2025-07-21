using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SwitchResumoPortasEntity : BaseImportacaoEntity
    {
        public string nm_switch { get; set; }
        public string cd_ip { get; set; }
        public int qt_portas_desc { get; set; }
	    public int qt_portas_up { get; set; }
        public int qt_portas_livres { get; set; }
        public int qt_portas_trunk { get; set; }
    }
}
