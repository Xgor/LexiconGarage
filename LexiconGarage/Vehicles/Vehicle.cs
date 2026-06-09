using System.Drawing;

namespace LexiconGarage.Vehicles;

public class Vehicle(string registrationNumber)
{
    public string RegistrationNumber { get; } = registrationNumber;
    protected int wheelCount { get; set; } // Will most likely set in child classes
    protected string colorName { get; set; }

    public override string ToString()
    {
        return $"vehicle {RegistrationNumber} has {wheelCount} wheels and is {colorName}";
    }
}

