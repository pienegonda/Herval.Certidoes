using System;
using System.Data;
using Herval.Certidoes.Domain.Interfaces.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Herval.Certidoes.Infra.Data.Context
{
    public class DbContext : IDbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration, ILogger<DbContext> logger)
        {
            _connectionString = configuration.GetConnectionString("MySql");

            if (string.IsNullOrEmpty(_connectionString))
            {
                logger.LogError("ConnectionString MySql not loaded from configuration.");
                throw new Exception("ConnectionString MySql não carregada");
            }

            logger.LogInformation("ConnectionString MySql loaded successfully.");
        }

        public IDbConnection Connection => new MySqlConnection(_connectionString);
    }
}