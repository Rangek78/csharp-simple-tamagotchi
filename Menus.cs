using Newtonsoft.Json;
using Tamagotchi.Pokemons;

internal class Menus
{
    public static void MainMenu(Player player)
    {
        do
        {
            Console.WriteLine($"Welcome, {player.Name}. Select an option number to start.");
            Console.WriteLine("1. List Pókemon");
            Console.WriteLine("2. Show Pokémon details");
            Console.WriteLine("3. Adopt a Pokémon");
            Console.WriteLine("4. View adopted Pokémon");
            Console.WriteLine("q. Terminate program");

            Console.Write("\nYour option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListPokemon();
                    break;
                case "2":
                    SelectPokemon();
                    break;
                case "3":
                    var pokemon = AdoptPokemon();
                    if (pokemon == null)
                        Console.WriteLine("The Pokémon has been left alone");
                    else
                    {
                        player.Adopt(pokemon);
                        Console.WriteLine("The Pokémon has been added to your adopted list");
                    }
                    break;
                case "4":
                    ViewAdopted(player);
                    break;
                case "q":
                case "Q":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (true);
    }

    private static void ViewAdopted(Player player)
    {
        Console.WriteLine("Adopted Pokémon:");
        player.AdoptedPokemon();
    }

    private static Pokemon AdoptPokemon()
    {
        Console.WriteLine("Insert the Pokémon name or id for adoption");
        var input = Console.ReadLine();
        try
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(GetAPI.GetPokemon(input!, "pokemon"))!;
            if (pokemon.Name == null)
                throw new Exception("Invalid name or id");

            while (true)
            {
                Console.WriteLine("Choose an option to continue");
                Console.WriteLine($"1. Show more details about {pokemon.Name}");
                Console.WriteLine($"2. Adopt {pokemon.Name}");
                Console.WriteLine("q. Go back");

                Console.Write("\nYour option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        SpeciesDetails(pokemon);
                        break;
                    case "2":
                        return pokemon;
                    case "q":
                    case "Q":
                        return null!;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        return null!;
    }

    public static void ListPokemon()
    {
        Console.WriteLine("Here is a small list of Pokémon you could try choosing from:");
        PokemonList pokemonList = JsonConvert.DeserializeObject<PokemonList>(GetAPI.GetPokemon("", "pokemon"))!;
        foreach (var pokemon in pokemonList.Results!)
        {
            pokemon.PrintDetails();
        }

    }
    public static void SelectPokemon()
    {
        Console.WriteLine("Insert the Pokémon name or id to view its details");
        var input = Console.ReadLine();
        try
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(GetAPI.GetPokemon(input!, "pokemon"))!;
            if (pokemon.Name == null)
                throw new Exception("Invalid name or id");

            Console.WriteLine("\nDetails:");
            pokemon.ShowDetails();
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public static void SpeciesDetails(Pokemon pokemon)
    {
        Console.WriteLine("\nDetails:");
        pokemon.ShowDetails();
        try
        {
            Console.Write("Loading...\r");

            var result = GetAPI.GetPokemon(pokemon.Name!, "pokemon-species");

            // Console.SetCursorPosition(0, Console.CursorTop);

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
            {
                Console.WriteLine("Pokémon does not evolve" + "   ");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

}