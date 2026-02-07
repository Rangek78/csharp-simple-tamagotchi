using Tamagotchi.Pokemons;

internal class Menus
{
    public static void MainMenu()
    {
        do
        {
            Console.WriteLine("Welcome. Select an option number to start.");
            Console.WriteLine("1. List Pókemon");
            Console.WriteLine("2. Show Pokémon details");
            Console.WriteLine("q. Terminate program.");

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
    public static void ListPokemon()
    {
        Console.WriteLine("Summarized List of Pokémon:");
        GetAPI.GetPokemonList();
    }
    public static void SelectPokemon()
    {
        Console.WriteLine("Insert the Pokémon name or id to view its details");
        var input = Console.ReadLine();
        try
        {
            Pokemon pokemon = GetAPI.GetPokemon(input!);
            if (pokemon.Name != null)
            {
                Console.WriteLine("\nDetails:");
                pokemon.ShowDetails();
                return;
            }
            throw new Exception("Invalid name or id");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

}