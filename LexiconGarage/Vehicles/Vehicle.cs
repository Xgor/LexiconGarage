using System.Drawing;
using LexiconGarage.Interfaces;

namespace LexiconGarage.Vehicles;

public class Vehicle(string registrationNumber, uint wheelCount,string colorName) :IVehicle
{
    public string RegistrationNumber { get; } = registrationNumber;
    public uint WheelCount { get; } = wheelCount;
    public string ColorName { get; set; } = colorName;

    public override string ToString()
    {
        return $"vehicle {RegistrationNumber} has {WheelCount} wheels and is {ColorName}";
    }
}

