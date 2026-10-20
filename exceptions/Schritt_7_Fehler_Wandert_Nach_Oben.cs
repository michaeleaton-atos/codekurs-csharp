using System;

class Program
{
    static void Main()
    {
        try
        {
            A();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception gefangen:");
            Console.WriteLine(e.Message);
        }
    }

    static void A()
    {
        Console.WriteLine("A");
        B();
    }

    static void B()
    {
        Console.WriteLine("B");
        C();
    }

    static void C()
    {
        Console.WriteLine("C");
        throw new Exception("Fehler in C");
    }
}
