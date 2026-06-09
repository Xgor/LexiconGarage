using System.Collections;
using LexiconGarage.Vehicles;
namespace LexiconGarage.Models;

public class Garage<T>:IEnumerable<T> where T:Vehicle
{
    private T[] _vehicles;

    
    public Garage(int size)
    {
        _vehicles = new T[size];
    }
    
    /// <summary>
    /// Adds to _vehicles
    /// </summary>
    /// <param name="v"></param>
    /// <returns>return index if successfull, -1 if failed</returns>
    public int AddToEmpty(T vehicle)
    {
        if (TryGetFirstEmptyIndex(out int index))
        {
            _vehicles[index] = vehicle;
            return index;
        }
        return -1;
        
    }

    public bool Remove(int index)
    {
        if (index < 0 || index >= _vehicles.Length) throw new ArgumentOutOfRangeException();
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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

    public T this[int i]
    {
        get => _vehicles[i];
       // set => 
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