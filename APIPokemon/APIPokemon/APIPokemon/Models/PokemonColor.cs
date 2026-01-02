namespace APIPokemon.Models;

public class PokemonColor
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public ICollection<Pokemon>? Pokemons { get; set; }
}
