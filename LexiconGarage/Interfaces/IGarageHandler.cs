using LexiconGarage.Vehicles;

namespace LexiconGarage.Interfaces;

public interface IGarageHandler
{
   void CreateNewGarage(uint size);
   int AddVehicleToCurrentGarage(Vehicle vehicle);
   bool RemoveFromCurrentGarage(string registrationNr);
   IEnumerable<string> GetAllVehicleInformation();
   IEnumerable<KeyValuePair<string,int>> GetVehicleCount();
   void AutoFillGarage();
   bool HasGarage();
   Vehicle? FindByRegistrationPlate(string registrationNr);

   
   //string[] GetVehicleTypeVariables(Vehicle variable);
}