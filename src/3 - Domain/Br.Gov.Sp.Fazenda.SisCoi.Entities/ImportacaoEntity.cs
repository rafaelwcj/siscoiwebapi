using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities {
    public class ImportacaoEntity {
        public long ID;
        public DateTime DataImportacao;

        // Servidores
        public List<ServerEntity> Servidores { get; set; }

        // CMDB
        public List<CMDBEntity> CMDB { get; set; }

        // AV
        public List<AVEntity> AV { get; set; }

        /// <summary>
        /// AD: Servidores
        /// </summary>
        public List<ADEntity> AD { get; set; }

        /// <summary>
        /// AD: Workstations
        /// </summary>
        public List<ADEntity> ADWS { get; set; }

        // Net Backup 
        public List<NetBkpAgenteEntity> NETBKP_AGT { get; set; }
        public List<NetBkpPoliciesEntity> NETBKP_POL { get; set; }

        // SCCM
        public List<SCCMEntity> SCCM { get; set; }

        // VCenter
        public List<VCDiskEntity> VC_DISK { get; set; }
        public List<VCInfoEntity> VC_INFO { get; set; }
        public List<VCNetworkEntity> VC_NETWORK { get; set; }
        public List<VCPartitionEntity> VC_PARTITION { get; set; }

        // OPS
        public List<OpsListaEntity> OPS_LISTA { get; set; }
        public List<OpsExecucaoEntity> OPS_EXECUCAO { get; set; }

        // STORAGE
        public List<StorageEntity> STORAGE { get; set; }

        public List<ScomEntity> SCOM { get; set; }

        public List<CMDBWinEntity> CMDBWin { get; set; }

        public List<AlocacaoEntity> ALOCACAO_SQLSERVER { get; set; }

        public List<AlocacaoOracleEntity> ALOCACAO_ORACLE { get; set; }

        public List<AlocacaoDiscosSCCMEntity> ALOCACAO_DISCOS_SCCM { get; set; }

        // Datas das importacoes
        public DateTime DtImpAD { get; set; }
        public DateTime DtImpAV { get; set; }
        public DateTime DtImpCMDB { get; set; }
        public DateTime DtImpSCCM { get; set; }
        public DateTime DtImpNETBKP_AGT { get; set; }
        public DateTime DtImpNETBKP_POL { get; set; }
        public DateTime DtImpVC { get; set; }
        public DateTime DtImpVC_DISK { get; set; }
        public DateTime DtImpVC_INFO { get; set; }
        public DateTime DtImpVC_NETWORK { get; set; }
        public DateTime DtImpVC_PARTITION { get; set; }
        public DateTime DtImpOPS_LISTA { get; set; }
        public DateTime DtImpOPS_EXECUCAO { get; set; }
        public DateTime DtImpSTORAGE { get; set; }
        public DateTime DtImpSCOM { get; set; }
        public DateTime DtImpCMDBWin { get; set; }
        public DateTime DtImpWorkstations { get; set; }
        public DateTime DtImpAvWs { get; set; }
        public DateTime DtImpAdWs { get; set; }        

        /// <summary>
        /// Construtor
        /// </summary>
        public ImportacaoEntity()
        {
            this.Servidores = new List<ServerEntity>();
            this.CMDB = new List<CMDBEntity>();
            this.AV = new List<AVEntity>();
            this.AD = new List<ADEntity>();
            this.ADWS = new List<ADEntity>();
            this.NETBKP_AGT = new List<NetBkpAgenteEntity>();
            this.NETBKP_POL = new List<NetBkpPoliciesEntity>();
            this.SCCM = new List<SCCMEntity>();
            this.VC_DISK = new List<VCDiskEntity>();
            this.VC_INFO = new List<VCInfoEntity>();
            this.VC_NETWORK = new List<VCNetworkEntity>();
            this.VC_PARTITION = new List<VCPartitionEntity>();
            this.OPS_LISTA = new List<OpsListaEntity>();
            this.OPS_EXECUCAO = new List<OpsExecucaoEntity>();
            this.STORAGE = new List<StorageEntity>();
            this.SCOM = new List<ScomEntity>();
            this.CMDBWin = new List<CMDBWinEntity>();
            this.ALOCACAO_ORACLE = new List<AlocacaoOracleEntity>();
            this.ALOCACAO_SQLSERVER = new List<AlocacaoEntity>();
            this.ALOCACAO_DISCOS_SCCM = new List<AlocacaoDiscosSCCMEntity>();
        }
    }
}
