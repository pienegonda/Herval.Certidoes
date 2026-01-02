using System.Threading.Tasks;
using Herval.Certidoes.Domain.Entities;

namespace Herval.Certidoes.Domain.Interfaces.Services
{
    public interface IFeitosTrabalhistasService
    {
        Task<bool> BaixarFeitosTrabalhistas(Certidao certidao);
    }
}