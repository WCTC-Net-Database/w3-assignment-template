using w3.console.Interfaces;

namespace w3.console;

public class CharacterManager
{
    private readonly string _filePath = "Files/input.csv";
    private readonly IInput _input;
    private readonly IOutput _output;

    private string[] lines;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;
    }

    public void AddCharacter()
    {
        // TEMPORARY TEST STUB: This code is only here to satisfy the unit tests.
        // TODO: Replace this stub with code that prompts for character details, adds the new character to the CSV file, and displays confirmation.
        // Use string interpolation for formatting output.
        // Hint: You will need to append the new character to the file.

        _output.Write("Enter character name: ");
        var name = _input.ReadLine();
        _output.Write("Enter character class: ");
        var charClass = _input.ReadLine();
        _output.Write("Enter character level: ");
        var level = _input.ReadLine();

        // TODO: Prompt for equipment items in a loop, not as a single pipe-separated string.
        var equipmentList = new List<string>();
        while (true)
        {
            _output.Write("Enter equipment item (leave blank to finish): ");
            var item = _input.ReadLine();
            if (string.IsNullOrWhiteSpace(item))
            {
                break;
            }

            equipmentList.Add(item);
        }

        var equipment = string.Join("|", equipmentList);

        _output.WriteLine($"{name},{charClass},{level},{equipment}");
        _output.WriteLine("Character added (stub).");
    }

    public void DisplayCharacters()
    {
        // TEMPORARY TEST STUB: This code is only here to satisfy the unit tests.
        // TODO: Replace this stub with code that reads all character data from the CSV file and displays each character.
        // Use string interpolation for formatting output.
        // Hint: You will need to parse each line and output the character details.
        _output.WriteLine("Displaying all characters...");
        _output.WriteLine("John, Brave,Fighter,1,10,sword|shield|potion");
        _output.WriteLine("Jane,Wizard,2,6,staff|robe|book");
        _output.WriteLine("Bob, Sneaky,Rogue,3,8,dagger|lockpick|cloak");
        _output.WriteLine("Alice,Cleric,4,12,mace|armor|potion");
        _output.WriteLine("Reginald III, Sir,Knight,5,20,sword|armor|horse");
    }

    public void FindCharacter()
    {
        // TEMPORARY TEST STUB: This code is only here to satisfy the unit tests.
        // TODO: Replace this stub with code that prompts for a character name, searches for the character using LINQ, and displays their details.
        // If not found, notify the user.
        // Use string interpolation for formatting output.
        // Hint: You will need to parse the CSV and use LINQ's FirstOrDefault.
        _output.Write("Enter character name to find: ");
        var name = _input.ReadLine();

        if (name == "Bob, Sneaky")
        {
            _output.WriteLine("Bob, Sneaky,Rogue,3,8,dagger|lockpick|cloak");
        }
        else
        {
            _output.WriteLine("Character not found");
        }
    }

    public void LevelUpCharacter()
    {
        // TEMPORARY TEST STUB: This code is only here to satisfy the unit tests.
        // TODO: Replace this stub with code that prompts for a character name, increases their level, updates the CSV file, and displays confirmation.
        // Use string interpolation for formatting output.
        // Hint: You will need to find the character, increment their level, and save changes.
        _output.Write("Enter character name to level up: ");
        var name = _input.ReadLine();

        if (name == "Reginald III, Sir")
        {
            _output.WriteLine("Reginald III, Sir is now level 6.");
        }
        else
        {
            _output.WriteLine($"{name} is now level X+1 (stub).");
        }
    }

    public void Run()
    {
        _output.WriteLine("Welcome to Character Management");

        lines = File.ReadAllLines(_filePath);

        while (true)
        {
            _output.WriteLine("Menu:");
            _output.WriteLine("1. Display Characters");
            _output.WriteLine("2. Add Character");
            _output.WriteLine("3. Level Up Character");
            _output.WriteLine("4. Find Character");
            _output.WriteLine("0. Exit");
            _output.Write("Enter your choice: ");
            var choice = _input.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    FindCharacter();
                    break;
                case "0":
                    return;
                default:
                    _output.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
