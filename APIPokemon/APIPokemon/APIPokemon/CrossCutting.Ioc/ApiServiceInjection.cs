using APIPokemon.ExternalServices;
using APIPokemon.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APIPokemon.CrossCutting.Ioc;

public static class ApiServiceInjection
{
    //tudo o que precisar chamar fora, chama aqui 
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IPokeApiService, PokeApiService>(client =>
            {
                var uri = configuration.GetValue<string>("PokeApiSettings:Endpoint");
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromMinutes(3);
            });
    }

}
