using LexiconGarage.Interfaces;

namespace LexiconGarage.Models;

public class ConsoleUI : IUI
{
    private Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();
    private bool _programRunning;
    public ConsoleUI()
    {
        // Add test commands, Should be added externally with AddCommand later
        AddCommand("1", new ConsoleCommand(testCommand, "this is a testcommand"));
        AddCommand("q", new ConsoleCommand(ExitProgram, "Exit program"));
    }

    public void AddCommand(string key, ConsoleCommand command)
    {
        commands.Add(key,command);
    }
    
#region Setup
    public void Run()
    {
        _programRunning = true;
        while (_programRunning)
        {
            ProgramMainLoop();
        }
    }
#endregion

#region MainLoop

    public void ProgramMainLoop()
    {
        Console.WriteLine("What do you want to do");
        WriteAllCommands();
        string command = Console.ReadLine();
        ReadCommand(command);
    }
    
    public void WriteAllCommands()
    {
        foreach (var command in commands)
        {
            Console.WriteLine($"{command.Key}. {command.Value.description}");
        }
    }

    private void ReadCommand(string inputCommand)
    {
        if (commands.TryGetValue(inputCommand,out ConsoleCommand value))
        {
            value.ConsoleAction.Invoke();
        }
        else Console.WriteLine("Invalid command");
    }
    
#endregion

# region Commands
    public void testCommand()
    {
        Console.WriteLine("hello world");
    }

    public void ExitProgram()
    {
        Console.WriteLine("bye bye");
        _programRunning = false;
    }
    #endregion
}