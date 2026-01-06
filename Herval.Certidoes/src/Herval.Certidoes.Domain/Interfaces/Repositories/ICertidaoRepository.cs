using System.Collections.Generic;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Entities;

namespace Herval.Certidoes.Domain.Interfaces.Repositories
{
    public interface ICertidaoRepository
    {
        Task<IEnumerable<Certidao>> ListarCetidoesAsync();
        Task InserirCertidaoAsync(Certidao certidao);
    }
}