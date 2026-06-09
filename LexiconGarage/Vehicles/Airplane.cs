namespace LexiconGarage.Vehicles;

public class Airplane:Vehicle
{
    public Airplane(string registrationNumber): base(registrationNumber)
    {
        wheelCount = 2;
        colorName = "White";
    }
}