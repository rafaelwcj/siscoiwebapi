using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Br.Gov.Sp.Fazenda.SisCoi.Data.Repositories
{
    public class SolicitacaoBkpRepository : ISolicitacaoBkpRepository
    {
        IConfiguration _configuration;
        SqlConnection _conexao;

        public SolicitacaoBkpRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexao = new SqlConnection(new Utils.Crypto.CCrypt().XmlDecrypt(_configuration.GetConnectionString("SisCoi")));
        }

        public IEnumerable<SolicitacaoBkpEntity> Listar(string nomeServidor, int cdDepto)
        {
            return _conexao.Query<SolicitacaoBkpEntity>("sel_SolicBkp_ServDepto @serverName, @cd_depto",
                    new { serverName = nomeServidor, cd_depto = cdDepto});   
        }
    }
}
