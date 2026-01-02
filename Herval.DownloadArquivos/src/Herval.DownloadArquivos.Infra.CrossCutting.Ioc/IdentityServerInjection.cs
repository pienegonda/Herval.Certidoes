using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class IdentityServerInjection
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = configuration.GetValue<string>("Autenticacao:Endpoint");
                options.Audience = configuration.GetValue<string>("Autenticacao:Scope");
            });
        }
    }
}
