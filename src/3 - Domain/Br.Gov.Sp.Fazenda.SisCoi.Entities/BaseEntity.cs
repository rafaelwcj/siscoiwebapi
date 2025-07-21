using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    /// <summary>
    ///  
    /// </summary>
    public class BaseImportacaoEntity
    {
        public int Id;
        public DateTime DataImportacao;

        public virtual TipoImportacaoEntity getProperties()
        {
            TipoImportacaoEntity resultado = new TipoImportacaoEntity();
            resultado.Nome = this.GetType().Name;

            foreach (var prop in this.GetType().GetProperties())
                resultado.PropertyName.Add(prop.Name);

            return resultado;
        }
    }
}
