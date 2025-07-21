using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class SolicitacaoBkpHistEntity : SolicitacaoBkpEntity
    {
        public long? id_historico_bkp { get; set; }

        public DateTime? dt_historico { get; set; }
    }
}
