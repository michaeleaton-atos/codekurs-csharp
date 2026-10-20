// Online C# compiler (editor) for free
// Write and run C# online using this editor.

using System;

class Haus
{
    public string Art;
    public int Stockwerke;
}

class Program
{
    static void Main()
    {
        // value type
        int stockwerke1 = 5;
        int stockwerke2 = stockwerke1;

        stockwerke1 = 20;

        Console.WriteLine("Value Type:");
        Console.WriteLine("stockwerke1 = " + stockwerke1); // 20
        Console.WriteLine("stockwerke2 = " + stockwerke2); // 5


        // refrence type
        Haus haus1 = new Haus();
        haus1.Stockwerke = 2;

        Haus haus2 = haus1;

        haus2.Stockwerke = 30;

        Console.WriteLine();
        Console.WriteLine("Reference Type:");

        Console.WriteLine("haus1.Stockwerke = " + haus1.Stockwerke); // 30
        Console.WriteLine("haus2.Stockwerke = " + haus2.Stockwerke); // 30
    }
}
