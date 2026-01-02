using Herval.DownloadArquivos.Domain.Commands.Template.Listar;
using Herval.Notifications.Interfaces;
using MediatR;

namespace Herval.DownloadArquivos.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<Worker> _loggerConsole;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ILogger? _loggerGraylog;

        public Worker(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<Worker> logger,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _loggerConsole = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var _notificationContext = scope.ServiceProvider.GetRequiredService<INotificationContext>();

            _loggerGraylog = scope.ServiceProvider.GetRequiredService<ILogger>();

            try
            {
                LogOnLoggers(logger => logger?.LogInformation("Inicio do processamento: {time}", DateTimeOffset.Now));

                var diretorio = "https://arquivos.receitafederal.gov.br/dados/cnpj/dados_abertos_cnpj/2025-07/";
                var caminho = @"C:\Downloads";

                await _mediator.Send(new BaixarArquivosCommand(diretorio, caminho), stoppingToken);

                if (_notificationContext.HasNotifications)
                {
                    foreach (var item in _notificationContext.Notifications)
                        LogOnLoggers(logger => logger?.LogWarning(item.Message));
                }
            }
            catch (Exception e)
            {
                LogOnLoggers(logger => logger?.LogError(e, "Erro na execuo do robo"));
            }
            finally
            {
                LogOnLoggers(logger => logger?.LogInformation("Horrio de processamento: {time}", DateTimeOffset.Now));

                // When completed, the entire app host will stop.
                _hostApplicationLifetime.StopApplication();
            }
        }

        private void LogOnLoggers(Action<ILogger?> method)
        {
            var loggers = new[] { _loggerConsole, _loggerGraylog };

            foreach (var logger in loggers)
                method(logger);
        }
    }

}