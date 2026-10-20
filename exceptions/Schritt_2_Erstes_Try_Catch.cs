using System;

class Program
{
    static void Main()
    {
        int[] zahlen = { 1, 2, 3 };

        try
        {
            Console.WriteLine("A");
            Console.WriteLine(zahlen[10]);
            Console.WriteLine("B");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("Fertig");
    }
}
