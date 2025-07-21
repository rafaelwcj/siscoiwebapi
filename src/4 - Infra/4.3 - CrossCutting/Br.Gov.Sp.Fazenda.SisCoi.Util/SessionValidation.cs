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
    public static class SessionValidation  {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        public static bool sessaoAtiva(Microsoft.AspNetCore.Http.HttpContext contexto)
        {

            byte[] storageValue = null;
            if(contexto.Session.TryGetValue("isAuthenticated", out storageValue))
            {
                if(storageValue != null)
                {
                    return Encoding.UTF8.GetString(storageValue) == "true";
                }
            }

            return false;
        }
    }
}
