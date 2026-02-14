namespace Tamagotchi.View;

using Tamagotchi.Model;

partial class View
{
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

    public static void AdoptPokemonSubMenu(Pokemon pokemon)
    {
        Console.WriteLine("Choose an option to continue");
        Console.WriteLine($"1. Show more details about {pokemon.Name}");
        Console.WriteLine($"2. Adopt {pokemon.Name}");
        Console.WriteLine("q. Go back");
    }
}

