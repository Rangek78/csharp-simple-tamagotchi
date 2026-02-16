namespace Tamagotchi.Model;

using System.Timers;

internal class Mascot
{
    public string? Name { get; set; }
    public int Weight { get; set; }
    public List<AbilityDesc>? Abilities { get; set; }
    private int _satiation { get; set; }
    private int _mood { get; set; }
    private int _energy { get; set; }

    private Timer hungerTimer = new(60e3 * 5);
    private Timer sadnessTimer = new(60e3 * 5);
    private Timer sleepTimer = new(60e3 * 5);

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

    private int Mood
    {
        get => _mood;
        set
        {
            sadnessTimer.Stop();
            _mood = CheckRange(value);
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

    public Mascot()
    {
        InitializeTimers();
    }

    private void InitializeTimers()
    {
        hungerTimer.Elapsed += OnTimedEvent!;
        hungerTimer.Enabled = true;

        sadnessTimer.Elapsed += OnTimedEvent!;
        sadnessTimer.Enabled = true;

        sleepTimer.Elapsed += OnTimedEvent!;
        sleepTimer.Enabled = true;

        Satiation = 5;
        Mood = 5;
        Energy = 10;
    }

    public void PlayWithMascot()
    {
        Mood += 2;
        Satiation--;
        Energy--;
    }

    public void FeedMascot()
    {
        Satiation += 4;
        Mood += 3;
        Energy++;
    }

    public void PutMascotToSleep()
    {
        Energy = 10;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Pokémon: {Name}, Weight: {Weight}\nAbilities:");
        Abilities!.ForEach(ability => ability.DescribeAbility());
        foreach (var ability in Abilities)
        {
            Console.WriteLine(ability.DescribeAbility());
        }
        Console.WriteLine($"{Name} is {GetHumor()}, {GetSatiation()} and {GetEnergy()}");
    }

    public string GetHumor()
    {
        switch (Mood)
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
        Mood--;
    }
}