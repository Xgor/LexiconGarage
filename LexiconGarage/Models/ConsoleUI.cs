using LexiconGarage.Helpers;
using LexiconGarage.Interfaces;
using LexiconGarage.Records;
using LexiconGarage.Vehicles;

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
        AddCommand("1", new ConsoleCommand(CreateGarageCommand, "Create new Garage"));
        AddCommand("2", new ConsoleCommand(AutoFillGarage, "Auto Fill Garage"));

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
        Console.ReadKey();
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

    private void AddGarageExistsCommands()
    {
        AddCommand("3", new ConsoleCommand(ListAllVehiclesInGarage, "Print all Vehicles in garage"));
        AddCommand("4", new ConsoleCommand(ListGarageVechicleCountCommand, "Get count for every vehicles"));
        AddCommand("5", new ConsoleCommand(SearchByRegistrationNumberCommand, "Search vehicle by registration number"));
        AddCommand("6", new ConsoleCommand(RemoveVehicleCommand,"Remove vehicle by registration number")); 
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
        if(!_garageHandler.HasGarage()) AddGarageExistsCommands();;
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

    public void CreateGarageCommand()
    {
        if (_garageHandler.HasGarage())
        {
            if (!ConsoleHelper.AskYNQuestion("Already have a garage, do you want to override it (Y/N) "))
                return;
        }
        _garageHandler.CreateNewGarage(ConsoleHelper.ReadAndParseUInt("How many spaces should be in the garage?"));
        
        Console.WriteLine("Garage created");
    }

    public void ListGarageVechicleCountCommand()
    {
        foreach (KeyValuePair<string, int> keyValuePair in _garageHandler.GetVehicleCount())
        {
            Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
        }
    }

    private Vehicle? SearchByRegistrationNumber()
    {
        string number = ConsoleHelper.ReadAndParseString("Please Enter Registration Number");
        return _garageHandler.FindByRegistrationPlate(number);
    }

    private void SearchByRegistrationNumberCommand()
    {
        Vehicle vehicle = SearchByRegistrationNumber();
        if (vehicle != null)
        {
            Console.WriteLine($"Found {vehicle}");
        }
        else Console.WriteLine("Couldn't find vehicle with that registration number");
    }

    public void RemoveVehicleCommand()
    {
        Vehicle vehicle = SearchByRegistrationNumber();
        if (vehicle != null)
        {
            _garageHandler.RemoveFromCurrentGarage(vehicle.RegistrationNumber);
            Console.WriteLine($"Removed Vehicle {vehicle.RegistrationNumber} from garage.");
        }
        else Console.WriteLine("No vehicle found with that number to remove");
    }
    
    #endregion
}