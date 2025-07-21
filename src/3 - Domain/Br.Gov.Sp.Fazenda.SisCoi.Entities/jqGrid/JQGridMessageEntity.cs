using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.jqGrid
{
    public class JQGridMessageEntity
    {
        public bool Sucess { get; set; }
        public IList<string> Messages { get; set; }
        public long NewID { get; set; }

        public JQGridMessageEntity(bool Sucess, IList<string> Messages, long NewID)
        {
            this.Sucess = Sucess;
            this.Messages = Messages;
            this.NewID = NewID;
        }
    }
}
