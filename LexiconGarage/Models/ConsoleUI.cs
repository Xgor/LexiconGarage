using System.Reflection;
using LexiconGarage.Helpers;
using LexiconGarage.Interfaces;
using LexiconGarage.Records;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Models;

public class ConsoleUI : IUI
{
    private IGarageHandler _garageHandler;
    private Dictionary<string, ConsoleCommand> menuCommands = new Dictionary<string, ConsoleCommand>();
    private Dictionary<string, ConsoleCommand> selectVehicleCommands = new Dictionary<string, ConsoleCommand>();
    private bool _programRunning;
    public ConsoleUI(IGarageHandler garageHandler)
    {
        _garageHandler = garageHandler;
        // Add test commands, Should be added externally with AddCommand later
        AddCommand("q", new ConsoleCommand(ExitProgram, "Exit program"));
        AddCommand("create", new ConsoleCommand(CreateGarageCommand, "Create new Garage"));
        AddCommand("autofill", new ConsoleCommand(AutoFillGarage, "Auto Fill Garage"));
        
        fillSelectVehicleCommands();
    }

    public void AddCommand(string key, ConsoleCommand command)
    {
        menuCommands.Add(key,command);
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

    private void fillSelectVehicleCommands()
    {
        selectVehicleCommands.Add("car", new ConsoleCommand(AddCar, "Add car"));
        selectVehicleCommands.Add("airplane", new ConsoleCommand(AddAirplaine, "Add airplane"));
        selectVehicleCommands.Add("boat", new ConsoleCommand(AddBoat, "Add boat"));
        selectVehicleCommands.Add("bus", new ConsoleCommand(AddBus, "Add bus"));
        selectVehicleCommands.Add("motorcycle", new ConsoleCommand(AddMotorcycle, "Add motorcycle"));
    }
#endregion

#region MainLoop

    public void ProgramMainLoop()
    {
        Console.WriteLine("What do you want to do");
        WriteCommands(menuCommands);
        string command = Console.ReadLine();
        ReadCommand(command,menuCommands);
        Console.ReadKey();
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

    private void ReadCommand(string inputCommand, Dictionary<string,ConsoleCommand> commands)
    {
        string[] splitCommands = inputCommand.Split();
        
        if (splitCommands.Length > 0 && commands.TryGetValue(splitCommands.First(),out ConsoleCommand value))
        {
            
            value.ConsoleAction.Invoke(splitCommands.Skip(1).ToArray());
        }
        else Console.WriteLine("Invalid command");
    }

    private void AddGarageExistsCommands()
    {
        AddCommand("add", new ConsoleCommand(AddVehicleCommand, "Add vehicle to garage"));
        AddCommand("list", new ConsoleCommand(ListAllVehiclesInGarage, "Print all Vehicles in garage"));
        AddCommand("count", new ConsoleCommand(ListGarageVechicleCountCommand, "Get count for every vehicles"));
        AddCommand("find", new ConsoleCommand(SearchByRegistrationNumberCommand, "Search vehicle by registration number. Can write search ABC123"));
        AddCommand("remove", new ConsoleCommand(RemoveVehicleCommand,"Remove vehicle by registration number")); 
        AddCommand("filter", new ConsoleCommand(FilterSearch, "Search vehicles by filtering different values"));
    }
    
    
#endregion

# region Commands
    public void AddVehicleCommand(string[]? args = null)
    {
        if (_garageHandler.IsGarageFull())
        {
            Console.WriteLine("Garage is full, please remove before you add more");
            return;
        }
        WriteCommands(selectVehicleCommands);
        string command = Console.ReadLine();
        ReadCommand(command,selectVehicleCommands);
    }

    public (string, uint, string) GetBaseVehicleInfo(string[]? args = null)
    {
        string registrationNumber = "";
        while (true)
        {
            registrationNumber = ConsoleHelper.ReadAndParseString("Enter Registration Number");
            if (_garageHandler.RegistrationNumberIsInUse(registrationNumber))
                break;
            Console.WriteLine("Registration number is already in use. please try again.");
        }

        uint wheelCount = ConsoleHelper.ReadAndParseUInt("Enter Wheel count");
        string colorName = ConsoleHelper.ReadAndParseString("Enter vehicle color");
        return (registrationNumber, wheelCount, colorName);
    }

    public void AddCar(string[]? args = null)
    {
        var baseInfo = GetBaseVehicleInfo(args);
        Fuel? fuelType = null;
        do
        {
            // TODO get fuel type
            Console.WriteLine("Get fuel type");
            Console.WriteLine("1. Gasoline");
            Console.WriteLine("2. Diesel");
            Console.WriteLine("3. Ethanol");
            
            switch (Console.ReadLine())
            {
                case "1": 
                    fuelType = Fuel.Gasoline;
                    break;
                case "2": 
                    fuelType = Fuel.Diesel;
                    break;
                case "3": 
                    fuelType = Fuel.Ethanol;
                    break;
            } 
            
        } while (fuelType == null);

        Car car = new Car(baseInfo.Item1, baseInfo.Item2, baseInfo.Item3, fuelType?? Fuel.Gasoline);
        _garageHandler.AddVehicleToCurrentGarage(car);
        Console.WriteLine($"{car} created");
    }

    public void AddAirplaine(string[]? args = null)
    {
        var baseInfo = GetBaseVehicleInfo(args);

        uint engines = ConsoleHelper.ReadAndParseUInt("How many engines does the airplane have?");
        Airplane plane = new Airplane(baseInfo.Item1, baseInfo.Item2, baseInfo.Item3, engines);
        _garageHandler.AddVehicleToCurrentGarage(plane);
        Console.WriteLine($"{plane} created");
    }
    
    public void AddBoat(string[]? args = null)
    {
        var baseInfo = GetBaseVehicleInfo(args);
        float length = ConsoleHelper.ReadAndParseFloat("How long is the boat");

        Boat boat = new Boat(baseInfo.Item1, baseInfo.Item2, baseInfo.Item3, length);
        _garageHandler.AddVehicleToCurrentGarage(boat);
        Console.WriteLine($"{boat} created");
    }
    
    public void AddBus(string[]? args = null)
    {
        var baseInfo = GetBaseVehicleInfo(args);

        uint seatCount = ConsoleHelper.ReadAndParseUInt("How many seats are on the bus");
        Bus bus = new Bus(baseInfo.Item1, baseInfo.Item2, baseInfo.Item3, seatCount);
        _garageHandler.AddVehicleToCurrentGarage(bus);
        Console.WriteLine($"{bus} created");
    }
    
    public void AddMotorcycle(string[]? args = null)
    {
        var baseInfo = GetBaseVehicleInfo(args);

        uint cylinderSize = ConsoleHelper.ReadAndParseUInt("What is the cylinder size");
        Motorcycle motorcycle = new Motorcycle(baseInfo.Item1, baseInfo.Item2, baseInfo.Item3, cylinderSize);
        _garageHandler.AddVehicleToCurrentGarage(motorcycle);
        
        Console.WriteLine($"{motorcycle} created");
    }
    
    public void ExitProgram(string[]? args = null)
    {
        Console.WriteLine("bye bye");
        _programRunning = false;
    }

    public void AutoFillGarage(string[]? args = null)
    {
        if(!_garageHandler.HasGarage()) AddGarageExistsCommands();;
        _garageHandler.AutoFillGarage();
        Console.WriteLine("Garage Filled");
    }

    public void ListAllVehiclesInGarage(string[]? args = null)
    {
        foreach (string s in _garageHandler.GetAllVehicleInformation())
        {
            Console.WriteLine(s);
        }
    }

    public void CreateGarageCommand(string[]? args = null)
    {
        if (_garageHandler.HasGarage())
        {
            if (!ConsoleHelper.AskYNQuestion("Already have a garage, do you want to override it (Y/N) "))
                return;
        }

        uint size;
        if (args.Length > 0 && args[0] != null)
        {
            if (!uint.TryParse(args[0], out size))
                return;
        }
        else size = ConsoleHelper.ReadAndParseUInt("How many spaces should be in the garage?");
        _garageHandler.CreateNewGarage(size);
        
        if(_garageHandler.HasGarage()) AddGarageExistsCommands();;
        Console.WriteLine("Garage created");
    }

    public void ListGarageVechicleCountCommand(string[]? args = null)
    {
        foreach (KeyValuePair<string, int> keyValuePair in _garageHandler.GetVehicleCount())
        {
            Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
        }
    }

    private Vehicle? SearchByRegistrationNumber(string[]? args = null)
    {
        string number;
        if (args.Length > 0 && args[0] != null)
        {
            Console.WriteLine("hello world");
            number = args[0];
        }
        else
        {
            number = ConsoleHelper.ReadAndParseString("Please Enter Registration Number");
        }
        return _garageHandler.FindByRegistrationPlate(number);
    }

    private void SearchByRegistrationNumberCommand(string[] args = null)
    {
        Vehicle vehicle = SearchByRegistrationNumber(args);
        if (vehicle != null)
        {
            Console.WriteLine($"Found {vehicle}");
        }
        else Console.WriteLine("Couldn't find vehicle with that registration number");
    }

    public void RemoveVehicleCommand(string[] args = null)
    {
        Vehicle vehicle = SearchByRegistrationNumber();
        if (vehicle != null)
        {
            _garageHandler.RemoveFromCurrentGarage(vehicle.RegistrationNumber);
            Console.WriteLine($"Removed Vehicle {vehicle.RegistrationNumber} from garage.");
        }
        else Console.WriteLine("No vehicle found with that number to remove");
    }

    private void AddToFilterList(ref Dictionary<string, string> filterList)
    {
        Console.WriteLine("Filter by:");
        PropertyInfo[] properties = typeof(Vehicle).GetRuntimeProperties().ToArray();
        for (int i = 0; i < properties.Length; i++)
        {
            Console.WriteLine($"{i}. {properties[i]}");
        }

        uint input = ConsoleHelper.ReadAndParseUInt("What will you filter on");
        if (input < properties.Length)
        {
            PropertyInfo property = properties[input];
            string attribute = ConsoleHelper.ReadAndParseString("What value to filter with");
            filterList.Add(property.Name,attribute);
        }
        
    }
    
    public void FilterSearch(string[] args = null)
    {
        Dictionary<string, string> filterList = new Dictionary<string, string>();

        do
        {
            AddToFilterList(ref filterList);
        } while (ConsoleHelper.AskYNQuestion("Want to add another filter"));
        
        
        var result = _garageHandler.FilterBy(filterList);
        if (result.Count() > 0)
        {
            foreach (Vehicle vehicle in result)
            {
                Console.WriteLine(vehicle);
            }
        }
        else Console.WriteLine("Nothing matching these filter conditions.");
    }
    
    #endregion
}