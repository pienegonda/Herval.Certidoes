using APIPokemon.Settings;

namespace APIPokemon.CrossCutting.Ioc;

public static class SettingsInjection
{
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PokeApiSettings>(
            configuration.GetSection(nameof(PokeApiSettings))
        );

    }
}
