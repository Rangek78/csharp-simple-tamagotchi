namespace Tamagotchi.Controller;

using Newtonsoft.Json;
using Tamagotchi.Model;
using Tamagotchi.View;

internal class Controller
{
    public static void Play(Player player)
    {
        do
        {
            View.MainMenu(player);
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        View.ListPokemonMenu();
                        ListPokemon();
                        break;
                    case "2":
                        View.SelectPokemonMenu();
                        SelectPokemon();
                        break;
                    case "3":
                        View.AdoptPokemonMenu();
                        var pokemon = PokemonForAdoption();

                        var willAdopt = AdoptOrNot(pokemon);

                        if (!willAdopt)
                            Console.WriteLine("The Pokémon has been left alone");
                        else
                        {
                            player.Adopt(pokemon);
                            Console.WriteLine("The Pokémon has been added to your adopted list");
                        }
                        break;
                    case "4":
                        View.ViewAdoptedMenu(player);
                        ViewAdopted(player);
                        break;
                    case "q":
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadKey();
            Console.Clear();
        } while (true);
    }

    private static void ViewAdopted(Player player)
    {
        player.AdoptedPokemon();
    }

    private static Pokemon PokemonForAdoption()
    {
        var input = Console.ReadLine();
        Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(GetAPI.GetPokemon(input!, "pokemon"))!;
        if (pokemon.Name == null)
            throw new Exception("Invalid name or id");
        return pokemon;
    }

    public static bool AdoptOrNot(Pokemon pokemon)
    {
        while (true)
        {
            View.AdoptPokemonSubMenu(pokemon);
            Console.Write("\nYour option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    View.DetailsMenu();
                    SpeciesDetails(pokemon);
                    break;
                case "2":
                    return true;
                case "q":
                case "Q":
                    return false;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    public static void ListPokemon()
    {
        PokemonList pokemonList = JsonConvert.DeserializeObject<PokemonList>(GetAPI.GetPokemon("", "pokemon"))!;
        foreach (var pokemon in pokemonList.Results!)
            pokemon.PrintDetails();
    }
    public static void SelectPokemon()
    {
        var input = Console.ReadLine();
        Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(GetAPI.GetPokemon(input!, "pokemon"))!;
        if (pokemon.Name == null)
            throw new Exception("Invalid name or id");

        View.DetailsMenu();
        pokemon.ShowDetails();
    }

    public static void SpeciesDetails(Pokemon pokemon)
    {
        pokemon.ShowDetails();
        Console.Write("Loading...\r");

        var result = GetAPI.GetPokemon(pokemon.Name!, "pokemon-species");

        PokemonSpecies pokemonSpecies = JsonConvert.DeserializeObject<PokemonSpecies>(result)!;
        if (pokemonSpecies.Name == null)
            throw new Exception("Invalid name or id");

        Console.Write($"Pokemón found: {pokemonSpecies.Name}");
        if (pokemonSpecies.EvolvesFrom != null)
            Console.Write($" (evolves from: {pokemonSpecies.EvolvesFrom.Name})");

        Console.WriteLine();

        Console.Write("Loading evolution chain...\r");
        EvolutionChain chain = JsonConvert.DeserializeObject<EvolutionChain>(GetAPI.GetPokemon(pokemonSpecies.EvoChainId!.Id!, "evolution-chain"))!;

        if (chain.EvoChain!.EvolvesTo!.Count > 0)
        {
            Console.WriteLine("Evolution chain for this Pokémon:");
            chain.GetEvolutionChain();
        }
        else
            Console.WriteLine("Pokémon does not evolve" + "   ");
    }
}