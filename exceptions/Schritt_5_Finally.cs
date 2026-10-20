using System;

class Program
{
    static void Main()
    {
        Console.Write("Gib eine Zahl ein: ");

        try
        {
            int zahl = int.Parse(Console.ReadLine());
            Console.WriteLine("Gültige Zahl: " + zahl);
        }
        catch (FormatException)
        {
            Console.WriteLine("Falsches Format.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Die Zahl ist zu gross.");
        }
        catch (Exception)
        {
            Console.WriteLine("Ein anderer Fehler ist aufgetreten.");
        }
        finally
        {
            Console.WriteLine("Eingabe beendet");
        }
    }
}
