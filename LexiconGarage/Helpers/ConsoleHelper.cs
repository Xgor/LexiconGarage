using System.Globalization;
using System.Numerics;
using LexiconGarage.Vehicles;

namespace LexiconGarage.Helpers;

public static class ConsoleHelper
{
    //where T: IBinaryInteger<T> 
    public static uint ReadAndParseUInt(string text,uint min = 0)
    {
        uint output;
        string line = "";
        while (true)
        {
            
            Console.WriteLine(text);
            line = Console.ReadLine() ?? String.Empty;
            if (uint.TryParse(line, out output))
            {
                if(output >= min)
                    return output;
                else
                    Console.WriteLine("Number is lower than allowed parameters");
            }
            else Console.WriteLine("Failed to parse, please add positive integer");
        } 
    }
    
    public static float ReadAndParseFloat(string text)
    {
        float output;
        string line = "";
        do
        {
            Console.WriteLine(text);
            line = Console.ReadLine() ?? String.Empty;
        } while (!float.TryParse(line,out output));
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
        Console.Write($"{text} (y/n): ");
        while (true)
        {
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (input)
            {
                case 'y': return true;
                case 'Y': return true;
                case 'j': return true;
                case 'J': return true;
                case 'n': return false;
                case 'N': return false;
            }
        }
        
    }
    
}