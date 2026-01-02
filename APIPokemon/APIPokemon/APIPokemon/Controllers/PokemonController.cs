using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using APIPokemon.Interfaces;
using APIPokemon.Models;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("[controller]")]

public class PokemonController : ControllerBase
{
    private readonly IPokeApiService _pokeApiService;

    public PokemonController(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pokemon>>> Get(int quantidade)
        => Ok(await _pokeApiService.ObterPokemonsQuantidadeAsync(quantidade));


    [HttpGet("{name}/cor")]
    public async Task<ActionResult<IEnumerable<PokemonColor>>> Get(string? name)
        => Ok(await _pokeApiService.ObterPokemonsCorAsync(name));


    [HttpGet("group-by-color")]
    public async Task<ActionResult<IEnumerable<PokemonColor>>> Get()
    {
        List<Pokemon> pokemons = (List<Pokemon>)await _pokeApiService.ObterPokemonsQuantidadeAsync(10);

        var cores = new List<PokemonColor>();

        foreach (var pokemon in pokemons)
        {
            var nomePokemon = pokemon.Name;
            if (nomePokemon != null)
            {
                var corPokemon = await _pokeApiService.ObterPokemonsCorAsync(nomePokemon);
                if (!cores.Any(c => c.Name == corPokemon.Name))
                {
                    corPokemon.Pokemons = new Collection<Pokemon> { pokemon };
                    cores.Add(corPokemon);
                }
                else
                {
                    cores.First(c => c.Name == corPokemon.Name)?.Pokemons?.Add(pokemon);
                }
            }
        }
        return cores;
    }
}


