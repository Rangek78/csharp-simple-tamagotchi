using Newtonsoft.Json;

namespace Tamagotchi.Model;

internal class EvolutionChain
{
    [JsonProperty("chain")]
    public Chain? EvoChain { get; set; }

    internal class Chain
    {
        [JsonProperty("evolves_to")]
        public List<Chain>? EvolvesTo { get; set; }

        [JsonProperty("species")]
        public Species? SpeciesDetails { get; set; }

        internal class Species
        {
            [JsonProperty("name")]
            public string? Name { get; set; }
        }

    }
    public void GetEvolutionChain()
    {
        var evolution = EvoChain;
        while (true)
        {
            Console.Write(evolution!.SpeciesDetails!.Name);
            if (evolution.EvolvesTo!.Count == 0)
            {
                return;
            }
            Console.Write(" -> ");
            evolution = evolution.EvolvesTo![0];
        }
    }
}
