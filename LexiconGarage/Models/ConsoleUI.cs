using System.Reflection;
using LexiconGarage.Helpers;
using LexiconGarage.Interfaces;
using LexiconGarage.Records;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Models;

public class ConsoleUI : IUI
{
    private IGarageHandler _garageHandler;
    private bool _programRunning;
    public ConsoleUI(IGarageHandler garageHandler)
    {
        _garageHandler = garageHandler;
        // Add test commands, Should be added externally with AddCommand later
        
      //  fillSelectVehicleCommands();
    }

    
  //  #region Setup

    #region MainLoop

    public void CommandMenu(Dictionary<string, ConsoleCommand> commands, string startline)
    {
        string command;
        do
        {
            Console.WriteLine(startline);
            WriteCommands(commands);
            command = Console.ReadLine();
        } while (!ReadCommand(command,commands));
    }
    
    public void WriteCommands( Dictionary<string, ConsoleCommand> commands)
    {
        const int sizeForKeyField = 16;
        foreach (var command in commands)
        {
            string whitespaces = new string(' ', sizeForKeyField - command.Key.Length);
            Console.WriteLine($"    {command.Key}{whitespaces}{command.Value.description}");
        }
    }

    public bool ReadCommand(string inputCommand, Dictionary<string,ConsoleCommand> commands)
    {
        string[] splitCommands = inputCommand.Split();
        
        if (splitCommands.Length > 0 && commands.TryGetValue(splitCommands.First(),out ConsoleCommand value))
        {
            value.ConsoleAction.Invoke(splitCommands.Skip(1).ToArray());
            return true;
        }
        Console.WriteLine("Invalid command");

        return false;
    }
#endregion
   
}