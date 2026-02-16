using Newtonsoft.Json;

namespace Tamagotchi.Model;

internal class Pokemon
{
    [JsonProperty("abilities")]
    public List<AbilityDesc>? Abilities { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("weight")]
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

}

internal class AbilityDesc
{
    [JsonProperty("ability")]
    public Desc? Ability { get; set; }

    [JsonProperty("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonProperty("slot")]
    public int Slot { get; set; }

    public string DescribeAbility()
    {
        return $"{Ability!.Name}, Slot {Slot}";
    }

    internal class Desc
    {

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
