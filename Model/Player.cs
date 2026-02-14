using Tamagotchi.Model;

internal class Player
{

    public string? Name { get; set; }
    private List<Mascot>? Adopted { get; }

    public Player(string? name)
    {
        Adopted = [];
        Name = name;
    }

    public Mascot GetMascot(int index)
    {
        return Adopted![index];
    }

    public int GetMascotCount()
    {
        return Adopted!.Count;
    }

    public void Adopt(Mascot pokemon)
    {
        Adopted!.Add(pokemon);
    }

    public void AdoptedPokemon()
    {
        if (Adopted!.Count == 0)
        {
            Console.WriteLine("[ No Pokémon have been adopted yet ]");
            return;
        }
        for (int i = 0; i < Adopted.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Adopted[i].Name} - Satiation: {Adopted[i].GetSatiation()}, Humor: {Adopted[i].GetHumor()}, Energy: {Adopted[i].GetEnergy()}");
        }
    }
}