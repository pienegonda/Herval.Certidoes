using System.Drawing;
using System.Text.Json;
using APIPokemon.Interfaces;
using APIPokemon.Models;
using APIPokemon.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace APIPokemon.ExternalServices;

public class PokeApiService : IPokeApiService
{
    private readonly HttpClient _httpClient;
    private readonly IOptionsMonitor<PokeApiSettings> _pokeApiSettings;

    public PokeApiService(HttpClient httpClient, IOptionsMonitor<PokeApiSettings> pokeApiSettings)
    {
        _httpClient = httpClient;
        _pokeApiSettings = pokeApiSettings;
    }


    public async Task<IEnumerable<Pokemon>> ObterPokemonsQuantidadeAsync(int quantidade)
    {
        var rota = String.Format(
                _pokeApiSettings.CurrentValue.ConsultaComLimite,
                quantidade
            );

        var response = await _httpClient.GetAsync(rota);
        if (!response.IsSuccessStatusCode)
            return default;

        PokemonResponse? pokemons = await MapeamentoRetorno(response);
        return pokemons.Results;
    }

    private async Task<PokemonResponse?> MapeamentoRetorno(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        var pokemons = JsonSerializer.Deserialize<PokemonResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return pokemons;
    }

    public async Task<PokemonColor> ObterPokemonsCorAsync(string name)
    {
        var rota = String.Format(
                _pokeApiSettings.CurrentValue.ConsultaCor,
                name
            );

        var response = await _httpClient.GetAsync(rota);
        if (!response.IsSuccessStatusCode)
            return default;

        var corPokemons = await MapeamentoCorRetorno(response);
        return corPokemons.Color;
    }

    private async Task<ColorResponse?> MapeamentoCorRetorno(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        var colorPokemons = JsonSerializer.Deserialize<ColorResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return colorPokemons;
    }

}
