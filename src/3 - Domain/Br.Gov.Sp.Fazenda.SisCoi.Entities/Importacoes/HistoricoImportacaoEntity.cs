using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes {
    /// <summary>
    /// 
    /// </summary>
    public class HistoricoImportacaoEntity {
        public int Id { get; set; }
        public Enums.TipoImportacao Tipo { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
    }
}
