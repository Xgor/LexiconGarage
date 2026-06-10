using LexiconGarage.Vehicles;

namespace LexiconGarage.Interfaces;

public interface IGarageHandler
{
   void CreateNewGarage(int size);
   int AddVehicle(Vehicle vehicle);
   void RemoveFromGarage(string registrationNr);
   Vehicle[] GetVehicleTypes();
   string[] GetAllVehicleInformation();
   IEnumerable<KeyValuePair<string,int>> GetVehicleCount();
   void AutoFillGarage();
   Vehicle FindByRegistrationPlate(string registrationNr);

   
   //string[] GetVehicleTypeVariables(Vehicle variable);
}