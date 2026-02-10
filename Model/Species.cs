using Newtonsoft.Json;

namespace Tamagotchi.Model;

internal class PokemonSpecies
{
    [JsonProperty("evolution_chain")]
    public EvoChain? EvoChainId;

    [JsonProperty("capture_rate")]
    public byte CaptureRate { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("is_baby")]
    public bool IsBaby { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("evolves_from_species")]
    public EvoFrom? EvolvesFrom { get; set; }

    internal class EvoChain
    {
        [JsonProperty("url")]
        public string? Url { get; set; }

        public string? Id
        {
            get
            {
                if (string.IsNullOrEmpty(Url))
                    return null;

                // safest + readable
                var segments = Url.TrimEnd('/').Split('/');
                return segments[^1];
            }
        }
    }
    internal class EvoFrom
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
