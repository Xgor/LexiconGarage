using LexiconGarage.Models;


namespace LexiconGarage.Interfaces;

public interface IUI
{

    //public void Init();
    public void Run();
    public void AddCommand(string key, ConsoleCommand command);
}