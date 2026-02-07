namespace Tamagotchi.Pokemons;

internal class PokemonList
{
    public List<ResultDetails>? Results { get; set; }
}
public class ResultDetails
{
    public string? Name { get; set; }
    public string? Url { get; set; }

    public void PrintDetails()
    {
        Console.WriteLine($"Name: {Name}, Url: {Url}");
    }
}
