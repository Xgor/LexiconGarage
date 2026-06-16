namespace LexiconGarage.Helpers;

public static class MyExtensions
{
    /// <summary>
    /// checks every T for 
    /// </summary>
    /// <param name="data">IEnumerable list to filter</param>
    /// <param name="property">Name of the variable to filter by</param>
    /// <param name="attribute">value to check equals to</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> FilterByPropertyAttribute<T>(this IEnumerable<T> data, string property, string attribute)
    {
        foreach(T value in data)
        {
            if (value.GetType().GetProperty(property).GetValue(value).ToString().Equals(attribute)) yield return value;
        }
    }
}