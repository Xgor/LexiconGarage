namespace LexiconGarage.Vehicles;

public class Boat : Vehicle
{
    public float Length { get; }
    public Boat(string registrationNumber,uint wheelCount, string colorName, float length) : base(registrationNumber, wheelCount, colorName)
    {
        Length = length;
    }
}