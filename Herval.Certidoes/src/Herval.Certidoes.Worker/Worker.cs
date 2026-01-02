using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Commands.BaixarCertidoes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Herval.Certidoes.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<Worker> _loggerConsole;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string[] _commandLineArgs;

        public Worker(
            IHostApplicationLifetime hostApplicationLifetime, 
            ILogger<Worker> loggerConsole, 
            IServiceScopeFactory serviceScopeFactory,
            string[] commandLineArgs)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _loggerConsole = loggerConsole;
            _serviceScopeFactory = serviceScopeFactory;
            _commandLineArgs = commandLineArgs;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            try
            {
                LogOnLoggers(logger => logger?.LogInformation("Inicio do processamento: {time}", DateTimeOffset.Now));

                await _mediator.Send(new BaixarCertidaoCommand(), cancellationToken);

            }
            catch (Exception e)
            {
                LogOnLoggers(logger => logger?.LogError(e, "Erro na execução do robô: {message}", e.Message));

                if (_mediator == null)
                    LogOnLoggers(logger => logger?.LogError("IMediator não foi resolvido pelo contêiner de injeção de dependência."));

                if (scope.ServiceProvider.GetService<IMediator>() == null)
                    LogOnLoggers(logger => logger?.LogError("IMediator não está registrado no contêiner de serviços."));

                throw;
            }
            finally
            {
                LogOnLoggers(logger => logger?.LogInformation("Horrio de processamento: {time}", DateTimeOffset.Now));

                // When completed, the entire app host will stop.
                _hostApplicationLifetime.StopApplication();
            }
        }

        private void LogOnLoggers(Action<ILogger> method)
        {
            var loggers = new[] { _loggerConsole };

            foreach (var logger in loggers)
                method(logger);
        }

    }
}