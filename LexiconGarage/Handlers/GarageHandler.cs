using System.Diagnostics;
using LexiconGarage.Helpers;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using LexiconGarage.Vehicles;
//using System.Security.Cryptography.RandomNumberGenerator;

namespace LexiconGarage.Handlers;

public class GarageHandler: IGarageHandler
{
    private Garage<Vehicle?>? _garage;

    // Might need major changes for multigarage support later
    public void CreateNewGarage(uint size)
    {
        _garage = new Garage<Vehicle>(size);
    }


    public int AddVehicleToCurrentGarage(Vehicle vehicle)
    {
        if (!HasGarage()) throw new NullReferenceException();
        if (FindByRegistrationPlate(vehicle.RegistrationNumber) == null)
        {
            //_garage?.AddToEmpty(vehicle);
            
            return _garage.AddToEmpty(vehicle);
        }
        else
        {
            throw new Exception("Not allowed to add vehicle with same registration number as already existing one");
        }
        return -1;
    }

    public bool RemoveFromCurrentGarage(string registrationNr)
    {
        if (!HasGarage()) throw new NullReferenceException();
        return _garage?.Remove(registrationNr) ?? false;
    }

    public IEnumerable<string> GetAllVehicleInformation()
    {
        if (!HasGarage()) throw new NullReferenceException();
        foreach (Vehicle? vehicle in _garage)
        {
            if(vehicle == null) continue;
            yield return vehicle.ToString();
        }
    }

    public IEnumerable<KeyValuePair<string,int>> GetVehicleCount()
    {
        if (!HasGarage()) throw new NullReferenceException();
        return _garage
            .Where(vehicle => vehicle != null)
            .CountBy(vehicle => vehicle.GetType().Name);
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

    public bool HasGarage()
    {
        return _garage != null;
    }

    public Vehicle? FindByRegistrationPlate(string registrationNr)
    {
        if (!HasGarage()) throw new NullReferenceException();
        return _garage?.GetByRegistrationNumber(registrationNr);
    }

    public bool RegistrationNumberIsInUse(string registrationNr)
    {
        if (!HasGarage()) throw new NullReferenceException();
        return null != _garage?.GetByRegistrationNumber(registrationNr);
    }

    public bool IsGarageFull()
    {
        if (!HasGarage()) throw new NullReferenceException();
        return _garage.IsFull;
    }


    // public void GetVehicleTypes
}