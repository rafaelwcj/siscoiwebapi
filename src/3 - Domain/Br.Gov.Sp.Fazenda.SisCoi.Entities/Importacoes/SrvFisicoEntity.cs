using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SrvFisicoEntity : BaseImportacaoEntity
    {
        public string nm_servidor { get; set; }
        public int qt_discos { get; set; }
        public string ds_os { get; set; }
        public string ds_aplicacao { get; set; }
        public string ds_atribuicao { get; set; }

        public decimal qt_total_gb { get; set; }
        public decimal qt_aloc_gb { get; set; }
        public decimal qt_livre_gb { get; set; }
        public decimal qt_raw_gb { get; set; }

        public decimal qt_total_tb { get; set; }
        public decimal qt_aloc_tb { get; set; }
        public decimal qt_livre_tb { get; set; }
        public decimal qt_raw_tb { get; set; }

        public List<SrvFisicoDiscoEntity> ListaDiscos { get; set; }
        public int ic_atlz { get; set; }

        public SrvFisicoEntity()
        {
            ListaDiscos = new List<SrvFisicoDiscoEntity>();
        }
    }
}
