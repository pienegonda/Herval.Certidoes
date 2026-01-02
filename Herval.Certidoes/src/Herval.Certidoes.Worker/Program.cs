using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Herval.Certidoes.Worker;
using Herval.Certidoes.Infra.CrossCutting.Ioc;
using Herval.Certidoes.Domain.Commands.BaixarCertidoes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Infra / Services / Repositories
        services.AddServices();

        // MediatR → aponta para o assembly onde estão os Commands / Handlers
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(BaixarCertidaoCommand).Assembly
            ));

        services.AddSettings();

        services.AddLogging(config =>
            config.AddConsole()
        );

        services.AddHostedService<Worker>();

        // Argumentos de linha de comando
        services.AddSingleton(args);
    })
    .Build();

await host.RunAsync();
