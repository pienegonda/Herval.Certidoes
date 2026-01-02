using Herval.DownloadArquivos.Worker;
using Herval.Graylog.Extensions;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var consoleAssembly = Assembly.GetExecutingAssembly();
        var configuration = hostContext.Configuration;

        services.AddLogging(configure => configure.AddConsole());
        services.AddGraylog(consoleAssembly);

        // services.AddServices(consoleAssembly);
        // services.AddMediatorHerval();
        // services.AddSettings();
        // services.AddApiServices(configuration);
        services.AddHostedService<Worker>();
        services.AddSingleton(args);
    })
    .Build();

await host.RunAsync();