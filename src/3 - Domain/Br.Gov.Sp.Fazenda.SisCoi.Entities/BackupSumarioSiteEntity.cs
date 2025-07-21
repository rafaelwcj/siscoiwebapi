using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class BackupSumarioSiteEntity
    {
        public string nm_site { get; set; }
        public long qt_rotinas_dia { get; set; }
        public long qt_recursos_dia { get; set; }
        public long qt_execs_dia { get; set; }
        public long qt_execs_mes { get; set; }
        public long qt_arqs_dia { get; set; }
        public decimal qt_gb_dia { get; set; }
        public decimal qt_tb_dia {  get { return qt_gb_dia / 1024; } }
        public decimal pc_sucesso_dia { get; set; }

        public long qt_arqs_mes { get; set; }
        public decimal qt_gb_mes { get; set; }
        public decimal qt_tb_mes { get { return qt_gb_mes / 1024; } }
    }
}

