using RestSharp;
using Tamagotchi.Pokemons;

internal class GetAPI
{
    public static Pokemon GetPokemon(string input)
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon");
        var request = new RestRequest(input, Method.Get);

        var response = client.Execute(request);
        if (response.IsSuccessStatusCode)
        {
            return client.Get<Pokemon>(request)!;
        }
        else
        {
            throw new Exception("Invalid Pokémon id or name");
        }
    }

    public static void GetPokemonList()
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon");
        var request = new RestRequest("", Method.Get);

        var response = client.Execute(request);
        if (response.IsSuccessStatusCode)
        {
            var pokemonList = client.Get<PokemonList>(request)!.Results!;
            foreach (var pokemon in pokemonList)
            {
                pokemon.PrintDetails();
            }
        }
        else
        {
            throw new Exception("Invalid Pokémon id or name");
        }
    }
}