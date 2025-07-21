using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class CMDBWinEntity : BaseImportacaoEntity
    {
        public string Nome { get; set; }
        public string Host { get; set; }
        public string IPGer { get; set; }
        public string IPRede { get; set; }
        public string Modelo { get; set; }
        public string LocalFisico { get; set; }
        public string LocalVirtual { get; set; }
        public string Tecnologia { get; set; }
        public string Ambiente { get; set; }
        public string SO { get; set; }
        public string Equipe { get; set; }
        public string Aplicacao { get; set; }
        public string Onda { get; set; }
        public string Dominio { get; set; }
        public string Responsavel { get; set; }
        public string RamalResp { get; set; }
        public string EmailResp { get; set; }
        public string Status { get; set; }
        public string URL { get; set; }
        public string IPVip { get; set; }
    }
}

