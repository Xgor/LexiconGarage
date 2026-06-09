using LexiconGarage.Models;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Handlers;

public class GarageHandler
{
    private Garage<Vehicle> _garage;

    public void CreateNewGarage(int size)
    {
        _garage = new Garage<Vehicle>(size);
    }
    
    
   // public void GetVehicleTypes
}