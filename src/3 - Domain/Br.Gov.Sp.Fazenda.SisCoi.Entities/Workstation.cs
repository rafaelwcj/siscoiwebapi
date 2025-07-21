using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class Workstation : WorkstationEntity
    {
        public string SccmOK { get; set; }
        public string ADOK { get; set; }
        public string AVOK { get; set; }

        public AVEntity AV { get; set; }
        public ADEntity AD { get; set; }

        public int RAMAtual { get; set; }
        public int RAMAnterior { get; set;  }
        
        public string ProcessadorAtual { get; set;  }
        public string ProcessadorAnterior { get; set; }

        public double EspacoTotalAtual { get; set; }
        public double EspacoTotalAnterior { get; set; }

        public double EspacoLivreAtual { get; set; }
        public double EspacoLivreAnterior { get; set; }

        public DateTime DataAnterior { get; set; }
        public DateTime DataAtual { get; set; }
        public string Compliance { get; set; }

        public int DiscoDif { get; set; }

        public Workstation()
        {
            AV = new AVEntity();
            AD = new ADEntity();
            DiscoDif = 0;
        }
    }
}
