
using System.Net;
using LexiconGarage;
using LexiconGarage.Handlers;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Program startup");
    
IGarageHandler garageHandler = new GarageHandler();
IUI ui = new ConsoleUI(garageHandler);
//ui.AddCommand();
ui.Run();

