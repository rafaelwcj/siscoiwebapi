using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.jqGrid
{
    public class SearchOptionsEntity
    {
        public SearchFilterEntity filters { get; set; }

        public bool _search { get; set; }

        public long nd { get; set; }

        public int rows { get; set; }

        public int page { get; set; }

        public string sidx  { get; set; }

        public string sord  { get; set; }

        public string searchField { get; set; }

        public string searchOper { get; set; }

        public string searchString { get; set; }

        public string[] colNames { get; set; }

        public SearchOptionsEntity()
        {
            this.filters = new SearchFilterEntity();
        }
    }
}
