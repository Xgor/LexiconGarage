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
            1 => new Airplane(registrationNumber),
            _ => new Car(registrationNumber)
        };
    }
}