using w3.console.Interfaces;

namespace w3.console;

class Program
{
    static void Main()
    {
        var input = new ConsoleInput();
        var output = new ConsoleOutput();

        // Create an instance of CharacterManager and call the Run method to start the program
        // This will enable the user to interact with the program and manage characters
        // The reason for creating an instance of CharacterManager is to separate the concerns of the program
        // This will allow for easier testing and maintenance of the program
        var manager = new CharacterManager(input, output);
        manager.Run();
    }
}

class ConsoleInput : IInput
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}

class ConsoleOutput : IOutput
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }
}