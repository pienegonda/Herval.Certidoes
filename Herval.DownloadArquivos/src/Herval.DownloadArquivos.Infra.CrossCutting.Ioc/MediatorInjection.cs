using FluentValidation;
using Herval.Mediator.PipelineBehavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Reflection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class MediatorInjection
    {
        private static Assembly DomainAssembly => AppDomain.CurrentDomain.Load("Herval.DownloadArquivos.Domain");

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(DomainAssembly));

            AssemblyScanner
                .FindValidatorsInAssembly(DomainAssembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
        }
    }
}
