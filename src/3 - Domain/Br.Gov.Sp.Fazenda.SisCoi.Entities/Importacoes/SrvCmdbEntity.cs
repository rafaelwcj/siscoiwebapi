using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SrvCmdbEntity : BaseImportacaoEntity
    {
        public long id { get; set; }
        public string nm_ic { get; set; }
        public string nm_site { get; set; }
        public string nm_ambiente { get; set; }
        public string nr_serie { get; set; }
        public string nm_fabricante { get; set; }
        public string nm_produto { get; set; }
        public string nm_dominio { get; set; }
        public string ds_aplicacao { get; set; }
        public string ds_atribuicao { get; set; }
        public string ds_status { get; set; }
        public string ds_onda { get; set; }
        public string cd_ipger { get; set; }
        public string cd_patrimonio { get; set; }
        public string ds_rack { get; set; }
        public string nm_equipe { get; set; }
        public string ds_posicao { get; set; }
        public string ic_virtual { get; set; }
        public string nm_regiao { get; set; }
        public DateTime dt_atlz { get; set; }
    }
}
