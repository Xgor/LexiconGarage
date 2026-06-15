using LexiconGarage.Vehicles;

namespace LexiconGarage.Helpers;

public static class RandomVehicleHelper
{
    private static Random _rand = new Random();
    public static string GenerateRandomLicenceNumber()
    {
        string output="";
        output += _rand.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3); 
        output += _rand.GetString("0123456789", 3);
        return output;
    }

    public static Vehicle GenerateRandomVehicle(string registrationNumber)
    {
        return _rand.Next(2) switch
        {
            1 => new Airplane(registrationNumber,RandUint(2,4)*2,"White",RandUint(2,4)),
            _ => new Car(registrationNumber,4,GenerateRandomColorName(),Fuel.Gasoline)
        };
    }

    private static uint RandUint(uint min, uint max)
    {
        return (uint)_rand.Next((int)min, (int)max);
    }

    public static string GenerateRandomColorName()
    {
        return _rand.Next(5) switch
        {
            1 => "Red",
            2 => "Blue",
            3 => "Black",
            4 => "Gray",
            _ => "White"
        };
    }
}