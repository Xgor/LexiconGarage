using LexiconGarage.Models;
using LexiconGarage.Records;


namespace LexiconGarage.Interfaces;

public interface IUI
{
    public void Run();
    public void AddCommand(string key, ConsoleCommand command);
}