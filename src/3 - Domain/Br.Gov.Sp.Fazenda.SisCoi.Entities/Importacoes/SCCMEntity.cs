using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SCCMEntity : BaseImportacaoEntity
    {
        public string Computador { get; set; }
        public string SerialNumber { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string SistemaOperacional { get; set; }
        public string ServicePack { get; set; }
        public string Unidade { get; set; }
        public float Disco { get; set; }
        public float Processador { get; set; }
        public float Memoria { get; set; }
    }
}
