using Br.Gov.Sp.Fazenda.SisCoi.Domain.Interfaces.Repositories;
using Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Br.Gov.Sp.Fazenda.SisCoi.Data.Repositories
{
    public class CmdbServicesRepository : ICmdbServicesRepository
    {
        IConfiguration _configuration;
        SqlConnection _conexao;

        public CmdbServicesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexao = new SqlConnection(new Utils.Crypto.CCrypt().XmlDecrypt(_configuration.GetConnectionString("SisCoi")));
        }

        public IEnumerable<CMDBServicesEntity> ListarAtivosPorDatas(string dtInicial, string dtFinal, string cnpj)
        {
            return _conexao.Query<CMDBServicesEntity>("SELECT distinct c.[ServicoCatalogoItil],c.[SafeNome],c.[Status],c.[TipoServico]," +
                "c.[ScrumKabanRespSubs],c.[ProjectOwner],c.[POSubst],c.[HospNew],c.[TecnoNew],c.[FabSWNew],c.[GrupoDesig] " +
                "FROM [dbo].[ImportacoesCMDBServices] c, [dbo].[Dominio] d " +
                "WHERE (UPPER(c.[Status]) = 'DEPLOYED' OR UPPER(c.[Status]) = 'IMPLANTADO') AND (c.[Data] Between '" + dtInicial.Split("-")[2] + "-" + dtInicial.Split("-")[1] + "-" + dtInicial.Split("-")[0] + " 00:00:00' AND '" + dtFinal.Split("-")[2] + "-" + dtFinal.Split("-")[1] + "-" + dtFinal.Split("-")[0] + " 23:59:59')" +
            (!string.IsNullOrEmpty(cnpj) ? " AND (c.[GrupoDesig] like '%' + d.GrupoDesignado + '%') AND (d.Cnpj = '" + cnpj + "')" : ""));
        }

        public IEnumerable<CMDBServicesEntity> ListarPorDatas(string dtInicial, string dtFinal)
        {
            return _conexao.Query<CMDBServicesEntity>("SELECT distinct [ServicoCatalogoItil],[SafeNome],[Status],[TipoServico]," +
                "[ScrumKabanRespSubs],[ProjectOwner],[POSubst],[HospNew],[TecnoNew],[FabSWNew],[GrupoDesig] " +
                "FROM [dbo].[ImportacoesCMDBServices] " +
                "WHERE [Data] Between '" + dtInicial.Split("-")[2] + "-" + dtInicial.Split("-")[1] + "-" + dtInicial.Split("-")[0] + " 00:00:00' AND '" + dtFinal.Split("-")[2] + "-" + dtFinal.Split("-")[1] + "-" + dtFinal.Split("-")[0] + " 23:59:59'");
        }
    }
}
