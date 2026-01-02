using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class ApiServiceInjection
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHttpClient<ICieloApiService, CieloApiService>(client => { 
            //    var uri = configuration.GetValue<string>("ApiCielo:Endpoint");
            //    client.BaseAddress = new Uri(uri);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //    client.UseCurrentAuthorizationToken(services);
            //})
            //.AddHttpMessageHandler<HttpClientMessageHandler>();
            return;
        }
    }
}
