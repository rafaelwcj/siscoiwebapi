using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    public class SwitchBrocadeEntity : BaseImportacaoEntity
    {
        public string nm_switch { get; set; }   // nome do switch (no nome do arq.)
        public string cd_ip { get; set; }       // IP do switch (no nome do arq.)

        public string nm_porta { get; set; }    // IDPorta
        public string ds_porta { get; set; }    // Descricao
        public int id_index { get; set; }       // Index
        public int id_port  { get; set; }       // Port
        public string cd_address { get; set; }  // Address
        public string ds_media { get; set; }    // Media
        public string ds_speed { get; set; }    // Speed
        public string ds_state { get; set; }    // State
        public string ds_proto { get; set; }    // Proto
        public string tp_porta { get; set; }    // Tipo_de_Porta
        public string cd_wwn { get; set; }      // Wwn
        public string cd_xid { get; set; }      // xId
    }
}
