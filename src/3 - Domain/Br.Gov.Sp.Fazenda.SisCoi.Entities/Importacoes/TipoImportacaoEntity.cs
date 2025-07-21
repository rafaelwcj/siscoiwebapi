using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes {
    /// <summary>
    /// 
    /// </summary>
    public class TipoImportacaoEntity {

        public List<string> PropertyName;
        public string Nome;

        /// <summary>
        /// 
        /// </summary>
        public TipoImportacaoEntity() {
            this.PropertyName = new List<string>();
        }
    }
}
