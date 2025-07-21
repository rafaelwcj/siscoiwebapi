using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.jqGrid
{    
    public class SearchFilterEntity
    {
        public string groupOp { get; set; }

        public IList<SearchRuleEntity> rules { get; set; }

        public SearchFilterEntity()
        {
            rules = new List<SearchRuleEntity>();
        }
    }
}
