using Herval.Certidoes.Domain.Interfaces.Context;
using Herval.Certidoes.Domain.Interfaces.Repositories;
using Herval.Certidoes.Domain.Interfaces.Services;
using Herval.Certidoes.Domain.Services;
using Herval.Certidoes.Infra.Data.Context;
using Herval.Certidoes.Infra.Data.Repositories;
using Herval.Certidoes.Infra.ExternalServices.FeitosTrabalhistas.Services;
using Herval.RPA.Sdk.Interfaces;
using Herval.RPA.Sdk.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Herval.Certidoes.Infra.CrossCutting.Ioc
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // External Services
            services.AddScoped<IFeitosTrabalhistasService, FeitosTrabalhistasService>();

            // Data Context
            services.AddScoped<IDbContext, DbContext>();

            // Repositories
            services.AddScoped<ICertidaoRepository, CertidaoRepository>();

            // Services
            services.AddScoped<ICertidaoService, CertidaoService>();

            // SDK Services
            services.AddScoped<IWebService, WebService>();
        }
    }
}
