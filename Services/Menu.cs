using Microsoft.EntityFrameworkCore;
using W10.Data;

namespace W10.Services;

public class Menu
{
    private readonly GameContext _gameContext;

    public Menu(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("\n=== Game Menu ===");
            Console.WriteLine("1. Display Rooms");
            Console.WriteLine("2. Display Characters");
            Console.WriteLine("3. Display Abilities");
            Console.WriteLine("4. Test Ability Usage");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayRooms();
                    break;
                case "2":
                    DisplayCharacters();
                    break;
                case "3":
                    DisplayAbilities();
                    break;
                case "4":
                    TestAbilityUsage();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    public void DisplayRooms()
    {
        var rooms = _gameContext.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void DisplayCharacters()
    {
        var characters = _gameContext.Characters.Include(c => c.Abilities).ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
                
                if (character.Abilities.Any())
                {
                    Console.WriteLine($"  Abilities:");
                    foreach (var ability in character.Abilities)
                    {
                        Console.WriteLine($"    - {ability.Name}: {ability.Description}");
                    }
                }
                else
                {
                    Console.WriteLine($"  No abilities.");
                }
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    public void DisplayAbilities()
    {
        var abilities = _gameContext.Abilities.ToList();
        if (abilities.Any())
        {
            Console.WriteLine("\n=== All Abilities ===");
            foreach (var ability in abilities)
            {
                var type = ability.GetType().Name;
                Console.WriteLine($"[{type}] {ability.Name} (ID: {ability.Id})");
                Console.WriteLine($"  Description: {ability.Description}");
                
                // Show ability-specific properties
                if (ability is W10.Models.Abilities.PlayerAbility playerAbility)
                {
                    Console.WriteLine($"  Shove Strength: {playerAbility.Shove}");
                }
                else if (ability is W10.Models.Abilities.GoblinAbility goblinAbility)
                {
                    Console.WriteLine($"  Taunt Level: {goblinAbility.Taunt}");
                }
            }
        }
        else
        {
            Console.WriteLine("No abilities available.");
        }
    }

    public void TestAbilityUsage()
    {
        var characters = _gameContext.Characters.Include(c => c.Abilities).ToList();
        
        if (!characters.Any())
        {
            Console.WriteLine("No characters available for testing.");
            return;
        }

        Console.WriteLine("\n=== Test Ability Usage ===");
        Console.WriteLine("Select attacker:");
        for (int i = 0; i < characters.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {characters[i].Name}");
        }
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int attackerChoice) || attackerChoice < 1 || attackerChoice > characters.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var attacker = characters[attackerChoice - 1];

        if (!attacker.Abilities.Any())
        {
            Console.WriteLine($"{attacker.Name} has no abilities!");
            return;
        }

        Console.WriteLine($"\nSelect target:");
        for (int i = 0; i < characters.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {characters[i].Name}");
        }
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int targetChoice) || targetChoice < 1 || targetChoice > characters.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var target = characters[targetChoice - 1];

        Console.WriteLine($"\nSelect ability:");
        for (int i = 0; i < attacker.Abilities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {attacker.Abilities.ElementAt(i).Name}");
        }
        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int abilityChoice) || abilityChoice < 1 || abilityChoice > attacker.Abilities.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var ability = attacker.Abilities.ElementAt(abilityChoice - 1);

        Console.WriteLine("\n--- Ability Activation ---");
        attacker.UseAbility(ability, target);
    }

}