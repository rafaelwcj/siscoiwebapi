using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class WorkstationEntity : BaseImportacaoEntity
    {
        /// <summary>
        /// ResourceID
        /// </summary>
        public long ResourceId { get; set; }

        /// <summary>
        /// Netbios_Name0,	Nome da Máquina
        /// </summary>
        public string NomeMaquina { get; set; }

        /// <summary>
        /// AD_Site_Name0,	Localidade
        /// </summary>
        public string Localidade { get; set; }

        /// <summary>
        /// TopConsoleUser0,	Último usuário
        /// </summary>
        public string UltimoUsuario { get; set; }

        /// <summary>
        /// Caption0,	SO
        /// </summary>
        public string SO { get; set; }

        /// <summary>
        /// CSDVersion0,	Service Pack
        /// </summary>
        public string ServicePack { get; set; }

        /// <summary>
        /// OSLanguage0,	Linguagem SO
        /// </summary>
        public string LinguagemSO { get; set; }

        /// <summary>
        /// Manufacturer0,	Fabricante
        /// </summary>
        public string Fabricante { get; set; }

        /// <summary>
        /// Model0,	Modelo
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Memoria__MB_,	Qtde de Memória
        /// </summary>
        public int RAM { get; set; }

        /// <summary>
        /// Slot,	Qtde de Slots
        /// </summary>
        public int Slots { get; set; }

        /// <summary>
        /// SystemType0,	Arquitetura SO
        /// </summary>
        public string ArquiteturaSO { get; set; }

        /// <summary>
        /// DeviceID0,	Drive de Disco
        /// </summary>
        public string UnidadeDisco { get; set; }

        /// <summary>
        /// Espaço_em_Disco__GB_,	Espaço Total
        /// </summary>
        public double EspacoTotal { get; set; }

        /// <summary>
        /// Espaço_Livre__GB_,	Espaço Livre
        /// </summary>
        public double EspacoLivre { get; set; }

        /// <summary>
        /// IP_Addresses0,	IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// ID,	Tipo de Dispositivo
        /// </summary>
        public string TipoDispositivo { get; set; }

        /// <summary>
        /// Name0,	Processador
        /// </summary>
        public string Processador { get; set; }
    }
}
