using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class BackupSumarioRetencao
    {
        public string nm_site { get; set; }
        public string ds_retencao { get; set; }
        public string nn_retencao { get; set; }
        public long qt_rotinas { get; set; }

        public string TempoRetencao
        {
            get
            {
                switch(nn_retencao)
                {
                    case "No":
                        return "Sem Retenção";

                    case "In":
                        return "Infinito";

                    default:
                        return ds_retencao;
                }
            }
        }
    }
}
