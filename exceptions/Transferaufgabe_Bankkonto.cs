using System;

// Eigene Exception
class NichtGenugGuthabenException : Exception
{
    public NichtGenugGuthabenException(string message)
        : base(message)
    {
    }
}


// Konto-Klasse
class Konto
{
    private decimal kontostand;

    public decimal Kontostand
    {
        get { return kontostand; }
    }

    public void Einzahlen(decimal betrag)
    {
        if (betrag <= 0)
        {
            throw new ArgumentException(
                "Der Betrag muss grösser als 0 sein."
            );
        }

        kontostand += betrag;
    }

    public void Abheben(decimal betrag)
    {
        if (betrag <= 0)
        {
            throw new ArgumentException(
                "Der Betrag muss grösser als 0 sein."
            );
        }

        if (betrag > kontostand)
        {
            throw new NichtGenugGuthabenException(
                "Nicht genug Guthaben."
            );
        }

        kontostand -= betrag;
    }
}


// Hauptprogramm
class Program
{
    static void Main()
    {
        Konto konto = new Konto();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== BANKKONTO ===");
            Console.WriteLine("1 - Einzahlen");
            Console.WriteLine("2 - Abheben");
            Console.WriteLine("0 - Beenden");
            Console.Write("Auswahl: ");

            string auswahl = Console.ReadLine();

            if (auswahl == "0")
            {
                break;
            }

            try
            {
                if (auswahl == "1")
                {
                    Console.Write("Betrag: ");

                    decimal betrag =
                        decimal.Parse(Console.ReadLine());

                    konto.Einzahlen(betrag);

                    Console.WriteLine("Einzahlung erfolgreich.");
                }
                else if (auswahl == "2")
                {
                    Console.Write("Betrag: ");

                    decimal betrag =
                        decimal.Parse(Console.ReadLine());

                    konto.Abheben(betrag);

                    Console.WriteLine("Abhebung erfolgreich.");
                }
                else
                {
                    Console.WriteLine("Ungültige Auswahl.");
                }
            }

            // Eingabefehler:
            // Benutzer gibt z.B. "abc" statt einer Zahl ein.
            catch (FormatException)
            {
                Console.WriteLine(
                    "Fehler: Bitte eine Zahl eingeben."
                );
            }

            // Eingabefehler / Regelverstoss:
            // Betrag ist 0 oder negativ.
            catch (ArgumentException e)
            {
                Console.WriteLine("Fehler: " + e.Message);
            }

            // Regelverstoss:
            // Es wird mehr abgehoben als vorhanden.
            catch (NichtGenugGuthabenException e)
            {
                Console.WriteLine("Fehler: " + e.Message);
            }

            // Unerwartete Exceptions können auf
            // Programmierfehler hinweisen.
            catch (Exception e)
            {
                Console.WriteLine(
                    "Programmierfehler: " + e.Message
                );
            }

            // Wird nach jeder Aktion ausgeführt.
            finally
            {
                Console.WriteLine(
                    "Kontostand: " +
                    konto.Kontostand + " CHF"
                );
            }
        }
    }
}
