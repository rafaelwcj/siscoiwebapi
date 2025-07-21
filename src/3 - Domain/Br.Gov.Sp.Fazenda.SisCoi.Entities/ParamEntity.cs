using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public enum TipoParamEntityEnum
    {
        INVALID = 0,
        INTEGER = 1,
        FLOAT,
        STRING
    }

    public class ParamEntity
    {
        public long id { get; set; }
        public string nm_chave { get; set; }
        public string ds_valor { get; set; }
        public DateTime dt_atualiz { get; set; }
        public TipoParamEntityEnum ds_tipo { get; set; }
    }
}
