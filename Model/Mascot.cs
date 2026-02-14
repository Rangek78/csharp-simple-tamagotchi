namespace Tamagotchi.Model;

using System.Timers;

internal class Mascot : Pokemon
{
    private int _satiation { get; set; }
    private int _humor { get; set; }
    private int _energy { get; set; }

    private Timer hungerTimer { get; set; }
    private Timer sadnessTimer { get; set; }
    private Timer sleepTimer { get; set; }

    private static int CheckRange(int value)
    {
        if (value > 10)
            return 10;
        else if (value < 0)
            return 0;
        else
            return value;
    }

    private int Satiation
    {
        get => _satiation;
        set
        {
            hungerTimer.Stop();
            _satiation = CheckRange(value);
            hungerTimer.Start();
        }

    }

    private int Humor
    {
        get => _humor;
        set
        {
            sadnessTimer.Stop();
            _humor = CheckRange(value);
            sadnessTimer.Start();
        }
    }

    private int Energy
    {
        get => _energy;
        set
        {
            sleepTimer.Stop();
            _energy = CheckRange(value);
            sleepTimer.Start();
        }
    }

    public Mascot(Pokemon pokemon)
    {
        Name = pokemon.Name;
        Abilities = pokemon.Abilities;
        Height = pokemon.Height;
        Id = pokemon.Id;
        Name = pokemon.Name;
        Weight = pokemon.Weight;

        var oneMinute = 60000;

        hungerTimer = new Timer(oneMinute * 5);
        hungerTimer.Elapsed += OnTimedEvent!;
        hungerTimer.Enabled = true;

        sadnessTimer = new Timer(oneMinute * 5);
        sadnessTimer.Elapsed += OnTimedEvent!;
        sadnessTimer.Enabled = true;

        sleepTimer = new Timer(oneMinute * 60);
        sleepTimer.Elapsed += OnTimedEvent!;
        sleepTimer.Enabled = true;

        Satiation = 5;
        Humor = 5;
        Energy = 10;
    }

    public void PlayWithMascot()
    {
        Humor += 2;
        Satiation--;
        Energy--;
    }

    public void FeedMascot()
    {
        Satiation += 4;
        Humor += 3;
        Energy++;
    }

    public void PutMascotToSleep()
    {
        Energy = 10;
    }

    public new void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine($"{Name} is {GetHumor()}, {GetSatiation()} and {GetEnergy()}");
    }

    public string GetHumor()
    {
        switch (Humor)
        {
            case int h when h > 7:
                return "happy";

            case int h when h > 3:
                return "bored";

            default:
                return "sad";
        }
    }

    public string GetSatiation()
    {
        switch (Satiation)
        {
            case int s when s > 8:
                return "full";

            case int s when s > 3:
                return "hungry";

            default:
                return "starving";
        }
    }

    public string GetEnergy()
    {
        switch (Energy)
        {
            case int e when e > 7:
                return "energetic";

            case int e when e > 3:
                return "tired";

            default:
                return "dog tired";
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Satiation--;
        Humor--;
    }
}