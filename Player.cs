using Tamagotchi.Pokemons;

internal class Player
{

    public string? Name { get; set; }
    private List<Pokemon>? Adopted { get; }

    public Player(string? name)
    {
        Adopted = [];
        Name = name;
    }


    public void Adopt(Pokemon pokemon)
    {
        Adopted!.Add(pokemon);
    }

    public void AdoptedPokemon()
    {
        foreach (var pokemon in Adopted!)
        {
            Console.WriteLine($"{pokemon.Name}");
        }
    }
}