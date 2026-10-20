using System;

class Haus
{
    public string Farbe;
}

class Program
{
    static void Main()
    {
        // value type
        int zahl1 = 10;
        int zahl2 = zahl1;

        zahl1 = 20;

        Console.WriteLine("Value Type:");
        Console.WriteLine("zahl1 = " + zahl1); // 10
        Console.WriteLine("zahl2 = " + zahl2); // 20

        // refrence type
        Haus haus1 = new Haus();
        haus1.Farbe = "Rot";

        Haus haus2 = haus1;

        haus2.Farbe = "Blau";

        Console.WriteLine("Reference Type:");
        Console.WriteLine("haus1.Farbe = " + haus1.Farbe); // Blau
        Console.WriteLine("haus2.Farbe = " + haus2.Farbe); // Blau
    }
}
