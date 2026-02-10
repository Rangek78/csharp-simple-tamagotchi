using Tamagotchi.Model;

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
        if (Adopted!.Count == 0)
        {
            Console.WriteLine("[ No Pokémon has been adopted yet ]");
            return;
        }
        foreach (var pokemon in Adopted!)
        {
            Console.WriteLine($"{pokemon.Name}");
        }
    }
}