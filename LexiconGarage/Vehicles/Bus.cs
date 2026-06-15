namespace LexiconGarage.Vehicles;

public class Bus : Vehicle
{
    public uint NumberOfSeats { get; }
    public Bus(string registrationNumber,uint wheelCount, string colorName, uint numberOfSeats) : base(registrationNumber, wheelCount, colorName)
    {
        NumberOfSeats = numberOfSeats;
    }
    public override string ToString()
    {
        return $"Bus {RegistrationNumber} has {NumberOfSeats} seats, {WheelCount} wheels that goes round and round and is {ColorName}";
    }
}