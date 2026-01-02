using System.Runtime.CompilerServices;

namespace APIPokemon.Models;

public class PokemonResponse
{
    public IEnumerable<Pokemon> Results { get; set; } = [];
}
