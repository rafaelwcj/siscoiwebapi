using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.ClientSisCoiWebApi
{
    public class SolicitacaoBkpAPI
    {
        public static IEnumerable<SolicitacaoBkpEntity> Get(string token, string servidor, int? depto)
        {
            IList<SolicitacaoBkpEntity> solicitacaoBkpEntities = new List<SolicitacaoBkpEntity>();

            try
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
                var config = builder.Build();

                string urlBase = config["ApplicationSettings:UrlBaseAPI"].ToString();
                var solBkpAPI = RestService.For<ISolicitacaoBkpAPI>(urlBase,
                           new RefitSettings
                           {
                               AuthorizationHeaderValueGetter = () =>
                                       Task.FromResult(token)
                           });

                var resultSolicitacaoBkp = solBkpAPI.Get(servidor, depto.Value).Result;

                var jsonSol = JsonSerializer.Serialize(resultSolicitacaoBkp.GetProperty("data"));
                var dataSolList = JsonSerializer.Deserialize<SolicitacaoBkpEntity[]>(jsonSol);

                if (dataSolList.Count() > 0)
                {
                    solicitacaoBkpEntities = dataSolList;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return solicitacaoBkpEntities;
        }
    }
}
