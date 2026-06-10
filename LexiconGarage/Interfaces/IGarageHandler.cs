using LexiconGarage.Vehicles;

namespace LexiconGarage.Interfaces;

public interface IGarageHandler
{
   void CreateNewGarage(int size);
   int AddVehicleToCurrentGarage(Vehicle vehicle);
   void RemoveFromCurrentGarage(string registrationNr);
   IEnumerable<string> GetAllVehicleInformation();
   IEnumerable<KeyValuePair<string,int>> GetVehicleCount();
   void AutoFillGarage();
   Vehicle FindByRegistrationPlate(string registrationNr);

   
   //string[] GetVehicleTypeVariables(Vehicle variable);
}