using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Herval.Certidoes.Domain.Entities;
using Herval.Certidoes.Domain.Interfaces.Context;
using Herval.Certidoes.Domain.Interfaces.Repositories;

namespace Herval.Certidoes.Infra.Data.Repositories
{
    public class CertidaoRepository : ICertidaoRepository
    {
        private readonly IDbContext _dbcontext;

        public CertidaoRepository(IDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Certidao>> ListarCetidoesAsync()
        {
            var connection = _dbcontext.Connection;

            var query = $@"
                SELECT 
                    Documento AS {nameof(Certidao.Documento)},
                    SiteId    AS {nameof(Certidao.SiteId)},
                    Link      AS {nameof(Certidao.Link)},
                    CAMINHO_ARQUIVO AS {nameof(Certidao.CaminhoArquivo)}
                FROM Certidoes";

            return await connection.QueryAsync<Certidao>(query);
        }

        public async Task InserirCertidaoAsync(Certidao certidao)
        {
            var connection = _dbcontext.Connection;

            var query = $@"
                INSERT INTO Certidoes (
                    {nameof(Certidao.Documento)},
                    {nameof(Certidao.SiteId)},
                    {nameof(Certidao.Link)},
                    {nameof(Certidao.CaminhoArquivo)},
                    {nameof(Certidao.NomeArquivo)}
                )
                VALUES (
                    @{nameof(Certidao.Documento)},
                    @{nameof(Certidao.SiteId)},
                    @{nameof(Certidao.Link)},
                    @{nameof(Certidao.CaminhoArquivo)},
                    @{nameof(Certidao.NomeArquivo)}
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

            await connection.QuerySingleAsync<int>(query, certidao);
        }
    }
}