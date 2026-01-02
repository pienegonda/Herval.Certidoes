using APIPokemon.Context;
using Microsoft.Extensions.Options;

namespace APIPokemon.CrossCutting.Ioc;

public static class AddApiDbContext
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>(options => 
        {
            options.UseSqlite("Data Source=pokeapi,db");
        });
    }
}
