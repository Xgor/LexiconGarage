using System.Diagnostics;
using LexiconGarage.Interfaces;
using LexiconGarage.Models;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Handlers;

public class GarageHandler: IGarageHandler
{
    private Garage<Vehicle> _garage;

    // Might need major changes for multigarage support later
    public void CreateNewGarage(int size)
    {
        _garage = new Garage<Vehicle>(size);
    }

    public int AddVehicle(Vehicle vehicle)
    {
        if (FindByRegistrationPlate(vehicle.RegistrationNumber) == null)
        {
            return _garage.AddToEmpty(vehicle);
        }
        
        throw new Exception("Not allowed to add vehicle with same registration number as already existing one");

        return -1;
    }

    public void RemoveFromGarage(string registrationNr)
    {
        throw new NotImplementedException();
    }

    public Vehicle[] GetVehicleTypes()
    {
        throw new NotImplementedException();
    }

    public string[] GetAllVehicleInformation()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<string,int>> GetVehicleCount()
    {
        return _garage.CountBy(vehicle => vehicle.GetType().ToString());
    }

    public void AutoFillGarage()
    {
        throw new NotImplementedException();
    }

    public Vehicle FindByRegistrationPlate(string registrationNr)
    {
        throw new NotImplementedException();
    }


    // public void GetVehicleTypes
}