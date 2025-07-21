using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class AnexoEntity
    {
        public Int64 Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Int64 ContentLength { get; set; }
        public byte[] Content { get; set; }

        public string ContentAsBase64
        {
            get { return System.Convert.ToBase64String(Content, 0, (int)ContentLength); }
        }
    }
}
