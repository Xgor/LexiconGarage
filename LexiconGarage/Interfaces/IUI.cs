using LexiconGarage.Models;
using LexiconGarage.Records;


namespace LexiconGarage.Interfaces;

public interface IUI
{
    void CommandMenu(Dictionary<string, ConsoleCommand> commands, string startline);
    void WriteCommands(Dictionary<string, ConsoleCommand> commands);
    public bool ReadCommand(string inputCommand, Dictionary<string, ConsoleCommand> commands);
}