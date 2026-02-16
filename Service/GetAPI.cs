namespace Tamagotchi.Service;

using RestSharp;

internal class GetAPI
{
    public static string GetPokemon(string input, string url)
    {
        var client = new RestClient("https://pokeapi.co/api/v2/" + url);
        var request = new RestRequest(input, Method.Get);

        var response = client.Execute(request);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Invalid Pokémon id or name");

        return response.Content!;
    }
}