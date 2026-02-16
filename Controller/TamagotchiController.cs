namespace Tamagotchi.Controller;

using Tamagotchi.Service;
using MapsterMapper;
using Newtonsoft.Json;
using Tamagotchi.Model;
using Tamagotchi.View;

internal class Controller
{
    private readonly IMapper mapper = new Mapper();

    public Controller() { }

    public void Play(Player player)
    {
        do
        {
            View.MainMenu(player);
            var option = char.ToLower(Console.ReadKey(true).KeyChar);

            try
            {
                switch (option)
                {
                    case '1':
                        View.ListPokemonMenu();
                        ListPokemon();
                        break;
                    case '2':
                        View.SelectPokemonMenu();
                        SelectPokemon();
                        break;
                    case '3':
                        View.AdoptPokemonMenu();
                        var pokemon = PokemonForAdoption();
                        var willAdopt = AdoptOrNot(pokemon);

                        if (!willAdopt)
                            Console.WriteLine("The Pokémon has been left alone");
                        else
                        {
                            player.Adopt(mapper.Map<Mascot>(pokemon));
                            Console.WriteLine("The Pokémon has been added to your adopted list");
                        }
                        break;
                    case '4':
                        View.ViewAdoptedMenu(player);
                        ViewAdopted(player);
                        if (player.GetMascotCount() > 0)
                        {
                            View.SelectAdoptedMenu();
                            var mascot = SelectAdopted(player);
                            if (mascot != null)
                                TakeActionOnAdopted(mascot);

                            Console.Clear();
                            continue;
                        }
                        break;
                    case 'q':
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

    private static void TakeActionOnAdopted(Mascot mascot)
    {
        while (true)
        {
            View.TakeActionOnAdoptedMenu(mascot);
            Console.Write("\nYour option: ");
            var option = char.ToLower(Console.ReadKey(true).KeyChar);

            switch (option)
            {
                case '1':
                    mascot.FeedMascot();
                    Console.WriteLine($"{mascot.Name} has been fed - it is {mascot.GetSatiation()}");
                    break;
                case '2':
                    mascot.PlayWithMascot();
                    Console.WriteLine($"You played with {mascot.Name} - it is {mascot.GetHumor()}");
                    break;
                case '3':
                    mascot.PutMascotToSleep();
                    Console.WriteLine($"{mascot.Name} has slept and is now awaken - it is {mascot.GetEnergy()}");
                    break;
                case 'q':
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void ViewAdopted(Player player)
    {
        player.AdoptedPokemon();
    }

    private static Mascot SelectAdopted(Player player)
    {
        while (true)
        {
            Console.Write("\nYour option: ");
            var option = char.ToLower(Console.ReadKey(true).KeyChar);

            if (option!.Equals('q'))
                return null!;

            try
            {
                var index = (int)char.GetNumericValue(option) - 1;
                return player.GetMascot(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid mascot number");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid option");
            }
            Console.ReadKey();
        }
    }

    private static Pokemon PokemonForAdoption()
    {
        var input = Console.ReadLine()!.ToLower();
        Console.Write("Loading...\r");
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
            var option = char.ToLower(Console.ReadKey(true).KeyChar);

            switch (option)
            {
                case '1':
                    View.DetailsMenu();
                    SpeciesDetails(pokemon);
                    break;
                case '2':
                    return true;
                case 'q':
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
        Console.Write("Loading...\r");
        PokemonList pokemonList = JsonConvert.DeserializeObject<PokemonList>(GetAPI.GetPokemon("", "pokemon"))!;
        foreach (var pokemon in pokemonList.Results!)
            pokemon.PrintDetails();
    }
    public static void SelectPokemon()
    {
        var input = Console.ReadLine()!.ToLower();
        Console.Write("\nLoading...\r");
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