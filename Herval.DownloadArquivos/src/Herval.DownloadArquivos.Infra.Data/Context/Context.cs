using FluentValidation.Results;
using Herval.DownloadArquivos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Herval.DownloadArquivos.Infra.Data.Context
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<FakeEntity> Fakes { get; set; }

        public Context(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseOracle(_configuration.GetConnectionString("HServices"), b => b.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.SetMaxIdentifierLength(30);

            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<ValidationFailure>();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    var currentType = property.GetColumnType();
                    var maxLength = property.GetMaxLength();

                    if (string.IsNullOrEmpty(currentType))
                    {
                        if (maxLength.HasValue)
                        {
                            property.SetColumnType($"VARCHAR({maxLength.Value})");
                        }
                        else
                        {
                            property.SetColumnType("VARCHAR(2000)");
                        }
                    }
                }
            }
        }
    }
}
