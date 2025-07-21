using Refit;
using System.Text.Json;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.ClientSisCoiWebApi
{
    public interface ILoginAPI
    {
        [Post("/Login")]
        Task<JsonElement> Post(object data);
    }
}
