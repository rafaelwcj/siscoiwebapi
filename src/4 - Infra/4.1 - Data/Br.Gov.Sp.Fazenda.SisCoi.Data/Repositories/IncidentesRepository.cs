using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Br.Gov.Sp.Fazenda.SisCoi.Data.Repositories
{
    public class IncidentesRepository : IIncidentesRepository
    {
        IConfiguration _configuration;
        SqlConnection _conexao;

        public IncidentesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexao = new SqlConnection(new Utils.Crypto.CCrypt().XmlDecrypt(_configuration.GetConnectionString("SisCoi")));
        }

        public IEnumerable<IncidentesEntity> Listar(string dtInicial, string dtFinal, string cnpj)
        {
            return _conexao.Query<IncidentesEntity>("SELECT convert(varchar(10), [Data], 103) Data,[IdIncidente],[ICServico],[Status],[StatusSLM],[Produto],[GrupoDesignado] " +
               "FROM [DB_SISCOI].[dbo].[ImportacoesIncidentes] " +
               "WHERE ([Data] Between '" + dtInicial.Split("-")[2] + "-" + dtInicial.Split("-")[1] + "-" + dtInicial.Split("-")[0] + " 00:00:00' AND '" + dtFinal.Split("-")[2] + "-" + dtFinal.Split("-")[1] + "-" + dtFinal.Split("-")[0] + " 23:59:59')" +
               (!string.IsNullOrEmpty(cnpj) ? " AND ([GrupoDesignado] in (SELECT [GrupoDesignado] FROM Dominio WHERE Cnpj = '" + cnpj + "'))" : ""));
        }
    }
}
