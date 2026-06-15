namespace LexiconGarage.Vehicles;

public class Airplane:Vehicle
{
    public uint NumberOfEngines { get; }
    public Airplane(string registrationNumber,uint wheelCount,string colorName,uint numberOfEngines): base(registrationNumber,wheelCount,colorName)
    {
        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return $"Plane {RegistrationNumber} has {WheelCount} wheels, {NumberOfEngines} engines and is {ColorName}";
    }
}