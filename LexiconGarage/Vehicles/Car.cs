namespace LexiconGarage.Vehicles;

public enum Fuel
{
    Gasoline = 1, 
    Ethanol,
    Diesel,
}
public class Car : Vehicle
{
    public Fuel FuelType { get; }
    public Car(string registrationNumber, uint wheelCount, string colorName, Fuel fuelType): base(registrationNumber,wheelCount, colorName)
    {
        FuelType = fuelType;
    }  
    public override string ToString()
    {
        return $"Car {RegistrationNumber} has {WheelCount} wheels and is {ColorName} and uses {Enum.GetName(FuelType)}";
    }
}