using System.Globalization;
using System.Numerics;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Helpers;

public static class ConsoleHelper
{
    //where T: IBinaryInteger<T> 
    public static uint ReadAndParseUInt(string text)
    {
        uint output;
        string line = "";
        do
        {
            Console.WriteLine(text);
            line = Console.ReadLine() ?? String.Empty;
        } while (!uint.TryParse(line,out output));
        return output;
    }
    
    public static string ReadAndParseString(string text)
    {
        CultureInfo cultures = new CultureInfo("en-US");
        string output = "";
        do
        {
            Console.WriteLine(text);
            output = Console.ReadLine() ?? String.Empty;
        } while (output.IsWhiteSpace());
        return output;
    }

    public static bool AskYNQuestion(string text)
    {
        Console.WriteLine(text);
        while (true)
        {
            char input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case 'y': return true;
                case 'j': return true;
                case 'n': return false;
            }
        }
    }
    
}