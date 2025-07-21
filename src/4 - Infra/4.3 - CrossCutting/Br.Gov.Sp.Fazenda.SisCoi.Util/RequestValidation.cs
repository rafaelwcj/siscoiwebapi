using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace Br.Gov.Sp.Fazenda.SisCoi.Utils {
    /// <summary>
    /// 
    /// </summary>
    public static class RequestValidation {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        public static bool validarRequisicao(Microsoft.AspNetCore.Http.HttpContext contexto)
        {
            // verifica sessions
            if (!SessionValidation.sessaoAtiva(contexto))
                return false;

            // verifica permissões

            return true;
        }

        public static bool ValidarUsuarioCoi(Microsoft.AspNetCore.Http.HttpContext ctx)
        {

            byte[] storageValue = null;
            if(ctx.Session.TryGetValue("UserArea", out storageValue))
            {
                if(storageValue != null)
                {
                    return Encoding.UTF8.GetString(storageValue) == "1";
                }
            }

            return false;
        }
    }
}
