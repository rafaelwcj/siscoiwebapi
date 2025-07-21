using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class TimeseriesDecimalRowEntity
    {
        public DateTime Data { get; set; }                  // Data referente 'a informacao
        public List<decimal?> Valores { get; set; }  // Volumetria de cada storage registrado

        public TimeseriesDecimalRowEntity()
        {
            Valores = new List<decimal?>();
        }
    }

    public class TimeseriesDecimalEntity
    {
        public int ColCount { get; set; }            // Quantidade de colunas
        public int RowCount { get; set; }            // Quantidade de registros
        public List<string> ColNames { get; set; }   // Nomes das colunas (p.ex. nomes dos equipamentos)

        // Lista de valores: [ { data de incidência, valor naquela data }, { ... } ]
        public List<TimeseriesDecimalRowEntity> Rows { get; set; }      

        public TimeseriesDecimalEntity()
        {
            ColNames = new List<string>();
            Rows = new List<TimeseriesDecimalRowEntity>();
        }
    }
}

