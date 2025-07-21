using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Models
{
    public class SolicitacaoBkpModels
    {
        public SolicitacaoBkpModels()
        {
            ic_ativo = true;
        }

        public long? id_solicitacao_bkp { get; set;  }

        public int? cd_depto { get; set; }

        public string ds_depto { get; set;  }
        public string cd_incidente { get; set;  } 
        public DateTime? dt_solicitacao { get; set;  } 
        public string nm_solicitante { get; set;  } 
        public string em_solicitante { get; set;  } 
        public string nm_gestor { get; set;  } 
        public string em_gestor { get; set;  } 
        public string nm_aplicacao { get; set;  } 
        public string cd_ramal { get; set;  } 
        public int? tp_host { get; set;  } 
        public bool? ic_rdm { get; set;  } 
        public string ds_politica { get; set;  } 
        public string nm_servidor { get; set;  } 
        public string ds_os { get; set;  } 
        public string ds_localizacao { get; set;  } 
        public bool? ic_mountpoint { get; set;  } 
        public string ds_ambiente { get; set;  } 
        public bool? ic_full_dom { get; set;  } 
        public bool? ic_full_seg { get; set;  } 
        public bool? ic_full_ter { get; set;  } 
        public bool? ic_full_qua { get; set;  } 
        public bool? ic_full_qui { get; set;  } 
        public bool? ic_full_sex { get; set;  } 
        public bool? ic_full_sab { get; set;  } 
        public bool? ic_diff_dom { get; set;  } 
        public bool? ic_diff_seg { get; set;  } 
        public bool? ic_diff_ter { get; set;  } 
        public bool? ic_diff_qua { get; set;  } 
        public bool? ic_diff_qui { get; set;  } 
        public bool? ic_diff_sex { get; set;  } 
        public bool? ic_diff_sab { get; set;  } 
        public bool? ic_log_dom { get; set;  } 
        public bool? ic_log_seg { get; set;  } 
        public bool? ic_log_ter { get; set;  } 
        public bool? ic_log_qua { get; set;  } 
        public bool? ic_log_qui { get; set;  } 
        public bool? ic_log_sex { get; set;  } 
        public bool? ic_log_sab { get; set;  }

        public int? qt_retencao { get; set; }

        public int? qt_freq_full { get; set;  } 
        public int? qt_freq_diff { get; set;  }
        public int? qt_freq_log { get; set; }
        
        public string hr_janela { get; set;  }
        public string hr_janela_diff { get; set; }
        public string hr_janela_log { get; set; }

        public string ds_retencao { get; set;  } 

        public string ds_obs { get; set;  } 

        public string nm_politica { get; set;  } 
        public DateTime? dt_criacao { get; set;  } 
        public string nm_atendente { get; set;  } 
        public int? tp_midia { get; set;  } 

        public bool? ic_accelerator { get; set;  } 
        public bool? ic_baremetal { get; set;  } 
        public bool? ic_dedup { get; set;  } 
        public bool? ic_compress { get; set;  } 
        public bool? ic_offsite { get; set;  } 
        public bool? ic_encrypt { get; set;  }

        public DateTime? dt_alteracao { get; set; }

        public string cd_usuario_alteracao { get; set; }

        public bool? ic_ativo { get; set; }

        public enum IndicadorStatusEnum
        {
            Pendente = 0,
            EmAndamento,
            Executada,
            Desativada
        };

        private IndicadorStatusEnum ic_status
        {
            get
            {
                if (ic_ativo != null && ic_ativo.Value == false)
                    return IndicadorStatusEnum.Desativada;

                if (dt_criacao != null)
                    return IndicadorStatusEnum.Executada;

                return String.IsNullOrWhiteSpace(nm_atendente) ? IndicadorStatusEnum.Pendente : IndicadorStatusEnum.EmAndamento;
            }
        }

        public string status
        {
            get
            {
                return ic_status.ToString();
            }
        }

        public string status_abrev
        {
            get
            {
                if (ic_status != IndicadorStatusEnum.EmAndamento)
                    return ic_status.ToString().Substring(0,1);

                return "A";
            }
        }

        public decimal? qt_volumetria { get; set; }
        public string ds_volumetria { get; set; }
    }
}
