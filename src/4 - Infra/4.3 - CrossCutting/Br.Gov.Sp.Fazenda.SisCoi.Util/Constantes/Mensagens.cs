using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Br.Gov.Sp.Fazenda.SisCoi.Entities.DTO;

namespace Br.Gov.Sp.Fazenda.SisCoi.Utils.Constantes {

    public class Caminhos {
        public static string GetCabecalhoSisCoi()
        {
            //return GetCaminhoImagens() + "header_bg.jpg";
            return string.Empty;
        }

        public static string GetCaminhoImagens()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Images\\";
        }

        public static string GetCaminhoExportacao() {
            return AppDomain.CurrentDomain.BaseDirectory + "temp\\";
        }
    }

    public class Mensagens {
        /// <summary>Erro genérico</summary>
        public static string MSG_RNF000_M000 = "Mensagem de erro genérica.";
        public static string MSG_RNF000_M404 = "Registro não encontrado.";        

        /* REQUISITO NÃO FUNCIONAL 001: Autenticação */
        /// <summary>Login ou senha inválidos</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_RNF001_M001 = new Entities.DTO.ReturnMessageDTO("MSG_RNF001_M001", "Login ou senha inválidos", "As credenciais informadas não são válidas. Verifique as informações digitadas e tente novamente.");
        /// <summary>Sessão expirada</summary>  
        public static Entities.DTO.ReturnMessageDTO MSG_RNF001_M002 = new Entities.DTO.ReturnMessageDTO("MSG_RNF001_M002", "Sessão expirada", "Sua sessão permanceu inativa por muito tempo. Por motivos de segurança, será preciso realizar o login novamente.");
        /// <summary>Acesso negado</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_RNF001_M003 = new Entities.DTO.ReturnMessageDTO("MSG_RNF001_M003", "Acesso negado", "Você não tem permissão para acessar esta consulta. Entre em contato com o administrador do sistema para esclarecimentos e eventuais liberações.");

        /* REQUISITO FUNCIONAL 001: Atualização de dados a partir do AD */
        /// <summary>Base atualizada a partir do AD</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ001_M001 = new Entities.DTO.ReturnMessageDTO("MSG_REQ001_M001", "Base atualizada a partir do AD", "A consulta e extração de dados do AD foi realizada com sucesso. Os informações estão disponíveis para as próximas consultas.");
        /// <summary>Consulta ao AD negada</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ001_M002 = new Entities.DTO.ReturnMessageDTO("MSG_REQ001_M002", "Consulta ao AD negada", "Não foi possível consultar o AD com suas credenciais. A senha utilizada nessa consulta é a de rede, não a do e-CPF.");

        /* REQUISITO FUNCIONAL 002: Atualização de dados a partir do CSV */
        /// <summary>Importação realizada com sucesso</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ002_M001 = new Entities.DTO.ReturnMessageDTO("MSG_REQ002_M001", "Importação realizada com sucesso", "A extração de dados do CSV foi realizada com sucesso. Os informações estão disponíveis para as próximas consultas.");

        /* REQUISITO FUNCIONAL 004: Manter Visões */
        /// <summary>Visão salva</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ004_M001 = new Entities.DTO.ReturnMessageDTO("MSG_REQ004_M001", "Visão salva", "A visão foi salva com sucesso.");
        /// <summary>Visão excluída</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ004_M002 = new Entities.DTO.ReturnMessageDTO("MSG_REQ004_M002", "Visão excluida", "A visão foi excluída com sucesso.");

        /* REQUISITO FUNCIONAL 005: Manter Histórido de Importações */
        /// <summary>Importação excluída</summary>
        public static Entities.DTO.ReturnMessageDTO MSG_REQ005_M001 = new Entities.DTO.ReturnMessageDTO("MSG_REQ005_M001", "Importação excluída", "A informações referentes à importação foram removidas da base histórica com sucesso.");

        public static Entities.DTO.ReturnMessageDTO MSG_RNF001_M004 = new ReturnMessageDTO("MSG_RNF001_M004", "Registro estático", "Este registro genérico é base para criação de outros relatórios e portanto não pode ser alterado ou excluído.");
    }
}
