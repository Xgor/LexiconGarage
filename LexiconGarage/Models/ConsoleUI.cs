using LexiconGarage.Interfaces;
using LexiconGarage.Records;

namespace LexiconGarage.Models;

public class ConsoleUI : IUI
{
    private IGarageHandler _garageHandler;
    private Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();
    private bool _programRunning;
    public ConsoleUI(IGarageHandler garageHandler)
    {
        _garageHandler = garageHandler;
        // Add test commands, Should be added externally with AddCommand later
        AddCommand("1", new ConsoleCommand(AutoFillGarage, "Auto Fill Garage"));
        AddCommand("2", new ConsoleCommand(ListAllVehiclesInGarage, "Print all Vehicles in garage"));
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

    public void AutoFillGarage()
    {
        _garageHandler.AutoFillGarage();
        Console.WriteLine("Garage Filled");
    }

    public void ListAllVehiclesInGarage()
    {
        foreach (string s in _garageHandler.GetAllVehicleInformation())
        {
            Console.WriteLine(s);
        }
    }
    
    #endregion
}