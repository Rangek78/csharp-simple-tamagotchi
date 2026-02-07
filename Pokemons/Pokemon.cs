namespace Tamagotchi.Pokemons;

internal class Pokemon
{
    public List<AbilityDesc>? Abilities { get; set; }

    public int Height { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Weight { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"Pokémon: {Name}, Weight: {Weight}\nAbilities:");
        Abilities!.ForEach(ability => ability.DescribeAbility());
        foreach (var ability in Abilities)
        {
            Console.WriteLine(ability.DescribeAbility());
        }
    }

    internal class AbilityDesc
    {
        public Desc? Ability { get; set; }
        public bool Is_Hidden { get; set; }
        public int Slot { get; set; }

        public string DescribeAbility()
        {
            return $"{Ability!.Name}, Slot {Slot}";
        }

        internal class Desc
        {
            public string? Name { get; set; }
            public string? Url { get; set; }
        }
    }
}
