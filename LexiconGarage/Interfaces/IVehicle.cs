namespace LexiconGarage.Interfaces;

public interface IVehicle
{

    string RegistrationNumber { get; }
    int WheelCount { get; }
    string ColorName{ get; set; }
}