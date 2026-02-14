namespace Tamagotchi.View;

using Tamagotchi.Model;

internal class View
{
    public static void MainMenu(Player player)
    {
        Console.WriteLine($"Welcome, {player.Name}. Select an option number to start.");
        Console.WriteLine("1. List Pókemon");
        Console.WriteLine("2. Show Pokémon details");
        Console.WriteLine("3. Adopt a Pokémon");
        Console.WriteLine("4. View adopted Pokémon");
        Console.WriteLine("q. Terminate program");

        Console.Write("\nYour option: ");
    }

    public static void ViewAdoptedMenu(Player player)
    {
        Console.WriteLine("Adopted Pokémon:");
    }

    public static void SelectAdoptedMenu()
    {
        Console.WriteLine("Select a mascot number to take action or type q to go back");
    }

    public static void TakeActionOnAdoptedMenu(Mascot mascot)
    {
        Console.WriteLine($"{mascot.Name} has been selected.");
        Console.WriteLine($"What do you want to do?");
        Console.WriteLine($"1. Feed {mascot.Name}");
        Console.WriteLine($"2. Play with {mascot.Name}");
        Console.WriteLine($"3. Put {mascot.Name} to sleep");
        Console.WriteLine($"q. Go back");
    }

    public static void AdoptPokemonMenu()
    {
        Console.WriteLine("Insert the Pokémon name or id for adoption");
    }

    public static void AdoptPokemonSubMenu(Pokemon pokemon)
    {
        Console.WriteLine("Choose an option to continue");
        Console.WriteLine($"1. Show more details about {pokemon.Name}");
        Console.WriteLine($"2. Adopt {pokemon.Name}");
        Console.WriteLine("q. Go back");
    }

    public static void ListPokemonMenu()
    {
        Console.WriteLine("Here is a small list of Pokémon you could try choosing from:");
    }
    public static void SelectPokemonMenu()
    {
        Console.WriteLine("Insert the Pokémon name or id to view its details");
    }

    public static void DetailsMenu()
    {
        Console.WriteLine("\nDetails:");
    }
}