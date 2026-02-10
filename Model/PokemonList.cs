using System.Text.Json.Serialization;

namespace Tamagotchi.Model;

internal class PokemonList
{
    [JsonPropertyName("results")]
    public List<ResultDetails>? Results { get; set; }
}
public class ResultDetails
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    public void PrintDetails()
    {
        Console.WriteLine($"Name: {Name}, Url: {Url}");
    }
}
