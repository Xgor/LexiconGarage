using System.Diagnostics;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using LexiconGarage.Vehicles;
//using System.Security.Cryptography.RandomNumberGenerator;

namespace LexiconGarage.Handlers;

public class GarageHandler: IGarageHandler
{
    private Garage<Vehicle> _garage;

    // Might need major changes for multigarage support later
    public void CreateNewGarage(int size)
    {
        _garage = new Garage<Vehicle>(size);
    }

    public int AddVehicleToCurrentGarage(Vehicle vehicle)
    {
        if (FindByRegistrationPlate(vehicle.RegistrationNumber) == null)
        {
            _garage.AddToEmpty(vehicle);
            return _garage.AddToEmpty(vehicle);
        }
        else
        {
            
            throw new Exception("Not allowed to add vehicle with same registration number as already existing one");
        }
        return -1;
    }

    public void RemoveFromCurrentGarage(string registrationNr)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetAllVehicleInformation()
    {
        foreach (Vehicle vehicle in _garage)
        {
            yield return vehicle.ToString();
        }
    }

    public IEnumerable<KeyValuePair<string,int>> GetVehicleCount()
    {
        return _garage.CountBy(vehicle => vehicle.GetType().ToString());
    }

    public void AutoFillGarage()
    {
        if (_garage == null) _garage = new Garage<Vehicle>(10);
        var rand = new Random();
        while (!_garage.IsFull)
        {
            string registrationNumber = "";
            registrationNumber += rand.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3);
            registrationNumber += rand.GetString("0123456789", 3);
            switch (rand.Next(2))
            {
                case 0:
                    _garage.AddToEmpty(new Car(registrationNumber));
                    break;
                case 1:
                    _garage.AddToEmpty(new Airplane(registrationNumber));
                    break;
            }
        }

    }

    public Vehicle FindByRegistrationPlate(string registrationNr)
    {
        return _garage.GetByRegistrationNumber(registrationNr);
    }


    // public void GetVehicleTypes
}