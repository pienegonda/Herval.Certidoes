using Herval.Certidoes.Domain.Entities;

namespace Herval.Certidoes.Domain.Interfaces.Services
{
    public interface IFeitosTrabalhistasService
    {
        bool BaixarFeitosTrabalhistas(Certidao certidao);
    }
}