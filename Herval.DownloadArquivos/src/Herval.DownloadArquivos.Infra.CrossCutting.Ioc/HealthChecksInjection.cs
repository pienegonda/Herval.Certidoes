using HealthChecks.UI.Client;
using Herval.DownloadArquivos.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class HealthChecksInjection
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration, Assembly apiAssembly)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<Context>("DbContext do EF Core")
                .AddOracle(connectionString: configuration.GetConnectionString("HServices"), name: "Banco de dados Oracle");

            //services.AddHealthChecks()
            //    .AddMongoDb(configuration["NoSqlDatabaseSettings:ConnectionString"]);

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint(apiAssembly.GetName().Name, "/health");
                setup.SetEvaluationTimeInSeconds(300);
                setup.SetMinimumSecondsBetweenFailureNotifications(300);
            })
            .AddInMemoryStorage();
        }

        public static void UseHealthChecks(this IEndpointRouteBuilder config)
        {
            config.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            config.MapHealthChecksUI();
        }
    }
}
