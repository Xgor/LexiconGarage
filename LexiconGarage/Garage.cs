using System.Collections;

namespace LexiconGarage;

using LexiconGarage.Vehicles;
public class Garage<T>:IEnumerable<T>
{
    private T[] _vehicles;

    /// <summary>
    /// Adds to _vehicles
    /// </summary>
    /// <param name="v"></param>
    /// <returns>true if successfull, false if failed</returns>
    
    public bool Add(T v)
    {
        if (TryGetFirstEmptyIndex(out int index))
        {
            _vehicles[index] = v;
            return false;
        }
        return false;
    }

    /// <summary>
    /// Finds first empty space in _vechicles 
    /// </summary>
    /// <param name="index">index trying to find empty</param>
    /// <returns>true if found. false if none found</returns>
    private bool TryGetFirstEmptyIndex(out int index)
    {
        for (index = 0; index < _vehicles.Length; index++)
        {
            if (_vehicles[index] == null)
            {
                return true;
            }
        }
        return false;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _vehicles.GetEnumerator();
    }
}