using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Enums {
    public static class Enumerators {
        // dummy
    }

    public static class Constants
    {
        public static TipoImportacao STG_MAX
        {
            get { return TipoImportacao.STG_DELL_COMPELLENT + 1; }
        }

        public static TipoImportacao TIPOIMPORTACAO_MAX
        {
            get { return TipoImportacao.TIPO_IMPORTACAO_MAX; }
        }
    }

    public enum TipoImportacao
    {
        CMDB = 0,
        AV = 1,
        AD = 2,         // AD, Servidores
        NETBKP_AGT = 3,
        NETBKP_POLICY = 6,
        SCCM = 4,
        VC_DISK = 5,
        VC_INFO = 7,
        VC_NETWORK = 8,
        VC_PARTITION = 9,
        OPS_LISTA = 10,
        OPS_EXECUCAO = 11,
        CONS_STG = 12,
        SCOM = 13,
        CMDBWIN = 14,
        SERVERINFO = 15,
        AD_WS = 16,       // AD, workstations
        SCCM_WS = 17,
        AV_WS = 18,

        ALOCACAO_SQLSERVER = 19,
        ALOCACAO_DISCOS_SCCM = 20,
        ALOCACAO_ORACLE = 21,

        STG_HUAWEI_5500,
        STG_HUAWEI_5800T,
        STG_FUJITSU_DX8700S2,
        STG_EMC_VMAX,
        STG_HP_3PAR8200,
        STG_DELL_COMPELLENT,

        VC_CLUSTER,
        VC_HOST,
        VC_DATASTORE,

        SF_RH6,
        SF_RH7,
        SF_WIN,
        SF_SOLARIS,

        SW_DESC_PORT,
        SW_FLOGI,
        SW_RESUMO_PORTAS,
        SW_BROCADE,
        SW_VELP,
        SW_DESC_VELP,

        TIPO_IMPORTACAO_MAX        
    }
}
