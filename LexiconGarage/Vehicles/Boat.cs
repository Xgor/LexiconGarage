namespace LexiconGarage.Vehicles;

public class Boat : Vehicle
{
    public float Length { get; }
    public Boat(string registrationNumber,uint wheelCount, string colorName, float length) : base(registrationNumber, wheelCount, colorName)
    {
        Length = length;
    }
    public override string ToString()
    {
        string wheelString = WheelCount > 0? $" is amphibious with {WheelCount} wheels," : "";
        return $"Boat {RegistrationNumber}{wheelString} is {Length}m long and is {ColorName}";
    }
}