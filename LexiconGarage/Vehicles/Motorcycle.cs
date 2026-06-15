namespace LexiconGarage.Vehicles;

public class Motorcycle: Vehicle
{
    public uint CylinderVolume { get; }
    public Motorcycle(string registrationNumber,uint wheelCount, string colorName, uint cylinderVolume) : base(registrationNumber, wheelCount, colorName)
    {
        CylinderVolume = cylinderVolume;
    }
}