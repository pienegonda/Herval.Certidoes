using System.Threading.Tasks;
using Herval.Certidoes.Domain.Entities;

namespace Herval.Certidoes.Domain.Interfaces.Services
{
    public interface ICertidaoService
    {
        bool BaixarFeitosTrabalhistas(Certidao certidao);
    }
}