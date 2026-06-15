namespace LexiconGarage.Interfaces;

public interface IVehicle
{

    string RegistrationNumber { get; }
    uint WheelCount { get; }
    string ColorName{ get; set; }
}