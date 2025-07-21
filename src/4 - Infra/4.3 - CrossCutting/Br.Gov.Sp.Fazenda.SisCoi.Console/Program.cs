using Br.Gov.Sp.Fazenda.SisCoi.Util.ClientSisCoiWebApi;
using System;

namespace Br.Gov.Sp.Fazenda.SisCoi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var token = LoginAPI.Conectar("rwcjesus", "Rafael.21");
                var solicitacaoBkpList = SolicitacaoBkpAPI.Get(token, "SRVNFE02", 47);

            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
