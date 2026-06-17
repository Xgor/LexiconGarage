using System.Net;
using LexiconGarage;
using LexiconGarage.Handlers;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Program startup");
    

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IUI,ConsoleUI>();
        services.AddSingleton<IGarageHandler,GarageHandler>();
        services.AddSingleton<IProgramManager, ProgramManager>();
    })
    .UseConsoleLifetime()
    .Build();

host.Services.GetRequiredService<IProgramManager>().Run();