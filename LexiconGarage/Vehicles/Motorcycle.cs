namespace LexiconGarage.Vehicles;

public class Motorcycle: Vehicle
{
    public uint CylinderVolume { get; }
    public Motorcycle(string registrationNumber,uint wheelCount, string colorName, uint cylinderVolume) : base(registrationNumber, wheelCount, colorName)
    {
        CylinderVolume = cylinderVolume;
    }
    
    public override string ToString()
    {
        return $"Motorcycle {CylinderVolume}cc {RegistrationNumber} has {WheelCount} wheels and is {ColorName}";
    }
}
