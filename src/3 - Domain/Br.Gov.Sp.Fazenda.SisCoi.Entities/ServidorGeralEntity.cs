using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class ServidorGeralEntity
    {
        public string nm_servidor { get; set; }
        public int qt_discos { get; set; }
        public string ds_os { get; set; }
        public string ds_atribuicao { get; set; }
        public decimal qt_total_tb { get; set; }
        public string tp_servidor { get; set; }
        public string nm_cluster { get; set; }
    }
}
