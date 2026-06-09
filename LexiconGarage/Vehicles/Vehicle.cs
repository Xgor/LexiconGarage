using System.Drawing;

namespace LexiconGarage.Vehicles;

public class Vehicle(string registrationNumber)
{
    public string RegistrationNumber { get; } = registrationNumber;
    public int wheelCount;
    public Color color;
}

