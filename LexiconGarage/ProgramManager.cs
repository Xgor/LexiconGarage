using System.Reflection;
using System.Text.RegularExpressions;
using LexiconGarage.Helpers;
using LexiconGarage.Interfaces;
using LexiconGarage.Records;
using LexiconGarage.Vehicles;

namespace LexiconGarage;

public class ProgramManager(IGarageHandler garageHandler, IUI ui) : IProgramManager
{
    private bool _programRunning;
    private IGarageHandler _garageHandler = garageHandler;
    private IUI _ui = ui;
    private Dictionary<string, ConsoleCommand> limitedMenuCommands = new Dictionary<string, ConsoleCommand>();
    private Dictionary<string, ConsoleCommand> menuCommands = new Dictionary<string, ConsoleCommand>();
    private Dictionary<string, ConsoleCommand> selectVehicleCommands = new Dictionary<string, ConsoleCommand>();
    
    #region setup
    public void Run()
    {
        SetupMainMenuCommands();
        fillSelectVehicleCommands();
        _programRunning = true;
        while (_programRunning)
        {
            ProgramMainLoop();
        }
    }
   
    private void SetupMainMenuCommands()
    {
        limitedMenuCommands.Add("create", new ConsoleCommand(CreateGarageCommand, "Create new Garage"));
        limitedMenuCommands.Add("autofill", new ConsoleCommand(AutoFillGarage, "Auto Fill Garage"));
        menuCommands = new Dictionary<string, ConsoleCommand>(limitedMenuCommands);
        menuCommands.Add("add", new ConsoleCommand(AddVehicleCommand, "Add vehicle to garage"));
        menuCommands.Add("list", new ConsoleCommand(ListAllVehiclesInGarage, "Print all Vehicles in garage"));
        menuCommands.Add("count", new ConsoleCommand(ListGarageVechicleCountCommand, "Get count for every vehicles"));
        menuCommands.Add("find", new ConsoleCommand(SearchByRegistrationNumberCommand, "Search vehicle by registration number. Can write search ABC123"));
        menuCommands.Add("remove", new ConsoleCommand(RemoveVehicleCommand,"Remove vehicle by registration number")); 
        menuCommands.Add("filter", new ConsoleCommand(FilterSearch, "Search vehicles by filtering different values"));
        // Set quit to end on all commands
        limitedMenuCommands.Add("quit", new ConsoleCommand(ExitProgram, "Exit program"));
        menuCommands.Add("quit", new ConsoleCommand(ExitProgram, "Exit program"));
        
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
        if (_garageHandler.HasGarage())
            _ui.CommandMenu(menuCommands, "What do you want to do in garages");
        else
            _ui.CommandMenu(limitedMenuCommands, "What do you want to do with garages");
      
        Console.ReadKey();
        Console.WriteLine();
    }
    #endregion

    #region Commands

   
 
    public void ExitProgram(string[]? args = null)
    {
        Console.WriteLine("bye bye");
        _programRunning = false;
    }
    
    public void AddVehicleCommand(string[]? args = null)
    {
        if (_garageHandler.IsGarageFull())
        {
            Console.WriteLine("Garage is full, please remove before you add more");
            return;
        }
        _ui.WriteCommands(selectVehicleCommands);
        string command = Console.ReadLine();
        _ui.ReadCommand(command,selectVehicleCommands);
    }

    public (string, uint, string) GetBaseVehicleInfo(string[]? args = null)
    {
        string registrationNumber = "";
        while (true)
        {
            registrationNumber = ConsoleHelper.ReadAndParseString("Enter Registration Number");
            if (!_garageHandler.RegistrationNumberIsInUse(registrationNumber))
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
   
    public void AutoFillGarage(string[]? args = null)
    {
        if (!_garageHandler.HasGarage())
        {
            _garageHandler.CreateNewGarage(10);
            Console.WriteLine("Automaticly created garage with 10 spaces");
        }
        if (!_garageHandler.IsGarageFull())
        {
            _garageHandler.AutoFillGarage();
            Console.WriteLine("Garage Filled");
        }
        else Console.WriteLine("Garage already full, Cannot fill with more");
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
        
        uint size = 0;
        if (args.Length > 0 && args[0] != null)
        {
            if (!uint.TryParse(args[0], out size))
            {
                Console.WriteLine("Invalid size");
                return;
            }
        }
        if (_garageHandler.HasGarage())
        {
            if (!ConsoleHelper.AskYNQuestion("Already have a garage, do you want to override it (Y/N) "))
                return;
        }
        
        if(size == 0) size = ConsoleHelper.ReadAndParseUInt("How many spaces should be in the garage?",1);
        
        _garageHandler.CreateNewGarage(size);
        
        Console.WriteLine($"Garage created with {size} spaces");
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
            Console.WriteLine($"{i}. {properties[i].Name}");
        }

        Console.WriteLine($"{properties.Length}. Type");

        
        uint input = ConsoleHelper.ReadAndParseUInt("What will you filter on");
        if (input < properties.Length)
        {
            PropertyInfo property = properties[input];
            string attribute = ConsoleHelper.ReadAndParseString($"In {property.Name} what value to filter by ({property.PropertyType})");
            filterList.Add(property.Name,attribute);
        }

        if (input == properties.Length)
        {
            Regex ex = new Regex("Car|Motorcycle|Airplane|Boat|Bus");
            
            string attribute = "";
            while (true)
            {
                
                attribute = ConsoleHelper.ReadAndParseString($"What type do you want to filter by (Car, Motorcycle, Airplane,Motorcycle,Bus,Boat)");
                if (ex.IsMatch(attribute))
                   break;
                Console.WriteLine("Not a match, try again");
            }
            
            filterList.Add("type",attribute.ToLower());
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
