
using LexiconGarage;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;

Console.WriteLine("Program startup");
IUI ui = new ConsoleUI();
ui.Init();
ui.Run();

