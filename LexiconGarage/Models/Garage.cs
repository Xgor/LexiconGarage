using System.Collections;
using LexiconGarage.Vehicles;
namespace LexiconGarage.Models;

public class Garage<T>:IEnumerable<T> where T:Vehicle
{
    private T?[] _values;
    public int valueCount { get; private set; }
    
    public Garage(int size)
    {
        _values = new T[size];
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
            _values[index] = vehicle;
            valueCount++;
            return index;
        }
        return -1;
        
    }

    public T? RemoveAndGet(string registrationNumber)
    {
        return RemoveAndGet(GetIndexByRegistrationNumber(registrationNumber)); // TODO Fix so it handles incorrect registration
    }

    public T? RemoveAndGet(int index)
    {
        if (index < 0 || index >= _values.Length) throw new ArgumentOutOfRangeException();
        try
        {
            if (_values[index] != null)
            {
                valueCount--;
                return _values[index];
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null; 
    }
    
    public bool Remove(int index)
    {
        return RemoveAndGet(index) != null;
    }

    /// <summary>
    /// Finds first empty space in _vechicles 
    /// </summary>
    /// <param name="index">index trying to find empty</param>
    /// <returns>true if found. false if none found</returns>
    private bool TryGetFirstEmptyIndex(out int index)
    {
        for (index = 0; index < _values.Length; index++)
        {
            if (_values[index] == null)
            {
                return true;
            }
        }
        return false;
    }

    public int GetIndexByRegistrationNumber(string registrationNumber)
    {
        T? v = GetByRegistrationNumber(registrationNumber);
        if (v != null)
            return _values.IndexOf(v);
        return -1;
    }

    public T? GetByRegistrationNumber(string registrationNumber)
    {
        return _values.FirstOrDefault(v => v.RegistrationNumber == registrationNumber);
    }

    public T GetByIndex(int index)
    {
        return _values[index];
    }
/*
    public T this[int i]
    {
        get => _vehicles[i];
        // set => _vehicles[i] = value;
    } */
    
    public IEnumerator<T> GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _values.GetEnumerator();
    }
}