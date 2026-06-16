using System.Collections;
using System.Reflection;
using LexiconGarage.Vehicles;
namespace LexiconGarage.Models;

public class Garage<T>:IEnumerable<T> where T:Vehicle?
{
    private T?[] _values;
    public int filledValueCount { get; private set; }
    
    public Garage(uint size)
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
            filledValueCount++;
            return index;
        }
        return -1;
    }

    public bool IsEmpty => filledValueCount == 0;
    public bool IsFull => _values.Length == filledValueCount;
    public int EmptySlots => _values.Length - filledValueCount;

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
                filledValueCount--;
                T output = _values[index];
                _values[index] = null;
                return output;
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

    public bool Remove(string registrationNumber)
    {
        return RemoveAndGet(registrationNumber) != null;
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
        if (IsEmpty) return null;
        return _values.FirstOrDefault(v => string.Equals(v?.RegistrationNumber, registrationNumber, StringComparison.InvariantCultureIgnoreCase));
    }

    public T GetByIndex(int index)
    {
        return _values[index];
    }
    
    
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T vehicle in _values)
        {
            yield return vehicle;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _values.GetEnumerator();
    }
    
    
}