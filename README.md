# Tamagotchi – C# Console Application
A C# console-based Tamagotchi simulation built using the MVC architecture pattern.
This project integrates data from the PokéAPI to generate and simulate Pokémon-inspired virtual pets.

## Features
MVC (Model–View–Controller) architecture

- Integration with the PokéAPI
- Pokémon-based Tamagotchi creatures
- Pet management system (e.g., hunger, happiness, energy)
- Time-based stat changes

## Architecture
This project follows the MVC (Model–View–Controller) design pattern:

### Model
- Handles the pet’s data (stats, state, Pokémon data from API)
- Contains business logic

### View
- Console-based user interface
- Displays menus, submenus and user options

### Controller
- Manages user input
- Updates the Model
- Refreshes the View accordingly

## API Integration
The application fetches Pokémon data from the PokéAPI, including:

- Name
- Base stats
- Evolution chain

Data is parsed from JSON into C# objects for use in the simulation.

# How to run
1. Clone the repository:
```bash
git clone https://github.com/Rangek78/csharp-simple-tamagotchi.git
cd csharp-simple-tamagotchi
```

2. Run the application:
```
dotnet run
```
