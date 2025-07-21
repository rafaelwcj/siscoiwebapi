using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.jqGrid
{
    public class SearchRuleEntity
    {
        // campo pelo qual sera efetuada a busca
        public string field { get; set; }       

        // operacao do jqGrid (cn = "LIKE", eq = "=" etc)
        public string op { get; set; }          

        // valor para efetuar comparacao do campo
        public string data { get; set; }        

        // campo provisionado para uso interno do sistema 
        // (é um PROBLEMA LEGADO, precisará ser consertado no futuro)
        public string value { get; set; }       
    }
}
