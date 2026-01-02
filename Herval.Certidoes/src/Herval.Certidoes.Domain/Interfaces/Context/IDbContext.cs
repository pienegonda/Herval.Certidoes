using System.Data;

namespace Herval.Certidoes.Domain.Interfaces.Context
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
    }
}