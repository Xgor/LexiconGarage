
using System.Net;
using LexiconGarage;
using LexiconGarage.Handlers;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Program startup");
    
GarageHandler garageHandler = new GarageHandler();
IUI ui = new ConsoleUI();
ui.Run();

