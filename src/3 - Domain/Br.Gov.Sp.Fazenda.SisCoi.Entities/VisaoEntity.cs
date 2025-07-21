using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class VisaoEntity
    {
        public int Id {get; set;}

        public string ReportOptions { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public AreaEntity Area { get; set; }

        public string TipoVisao { get; set; }

        public VisaoEntity()
        {
            this.Area = new AreaEntity();
        }
    }
}
