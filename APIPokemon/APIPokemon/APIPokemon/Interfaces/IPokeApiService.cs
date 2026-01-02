using APIPokemon.Models;

namespace APIPokemon.Interfaces;

public interface IPokeApiService
{
    Task<IEnumerable<Pokemon>> ObterPokemonsQuantidadeAsync(int quantidade);
    Task<PokemonColor> ObterPokemonsCorAsync(string nome);
}
 