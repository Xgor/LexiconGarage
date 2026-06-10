using System.Diagnostics;
using LexiconGarage.Helpers;
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
        while (!_garage.IsFull)
        {

            string registrationNumber = "";
            do
            {
                registrationNumber = RandomVehicleHelper.GenerateRandomLicenceNumber();
            } while (_garage.GetByRegistrationNumber(registrationNumber) != null);

            _garage.AddToEmpty(RandomVehicleHelper.GenerateRandomVehicle(registrationNumber));

        }
    }

    public Vehicle FindByRegistrationPlate(string registrationNr)
    {
        return _garage.GetByRegistrationNumber(registrationNr);
    }


    // public void GetVehicleTypes
}