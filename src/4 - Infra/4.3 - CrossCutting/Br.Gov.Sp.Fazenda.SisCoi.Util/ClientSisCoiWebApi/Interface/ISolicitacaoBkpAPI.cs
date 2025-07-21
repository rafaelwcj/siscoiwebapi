using Refit;
using System.Text.Json;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.ClientSisCoiWebApi
{
    public interface ISolicitacaoBkpAPI
    {
        [Get("/SolicitacaoBkp")]
        [Headers("Authorization: Bearer")]
        Task<JsonElement> Get(string servidor, int depto);
    }
}
