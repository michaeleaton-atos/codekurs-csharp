// Online C# compiler (editor) for free
// Write and run C# online using this editor.

using System;
using System.Collections.Generic;

public class Programm{
    public static void Main(string[] args) {
        int anzahl = 0;
        List<Kunde> kundenListe = new List<Kunde>();

        while (anzahl < 3)
        {
            Kunde kunde1 = new Kunde();
            Console.WriteLine ("Bitte geben sie den Kunden Namen ein");
            string kundenName = Console.ReadLine();

            Console.WriteLine ("Bitte geben sie den Kunden Nachnamen ein");
            string kundenNachName = Console.ReadLine();

            Console.WriteLine ("Bitte geben sie das Alter vom Kunden ein");
            string kundenAlterInput = Console.ReadLine();
            int.TryParse(kundenAlterInput, out int kundenAlter);

            Console.WriteLine ("Bitte geben sie den Umsatz vom Kunden ein");
            string kundenUmsatzInput = Console.ReadLine();
            int.TryParse(kundenUmsatzInput, out int kundenUmsatz);

            kunde1.name=kundenName;
            kunde1.nachname=kundenNachName;
            kunde1.alter=kundenAlter;
            kunde1.umsatz=kundenUmsatz;


            kundenListe.Add(kunde1)

            kunde1.KundeAnzeigen();
            anzahl++;
        }

        Kunde.KundenAnzahl(kundenListe);
    }
}

public class Kunde {
public string name;
public string nachname;
public int umsatz;
public int alter;

    public void KundeAnzeigen()
    {
        Console.WriteLine("Dieser Kunde heisst:"+this.name+" "+this.nachname+" ist "+this.alter+" Jahre alt und hat einen Umsatz von: "+this.umsatz);
    }

    public void KundeHinzufügen(List<Kunde> kundenListe)
    {


        this.name = kundenName;
        this.nachname = kundenNachName;
        this.umsatz = kundenUmsatz;
        this.alter = kundenAlter;

        kundenListe.Add(this);    
    }

    public static void KundenAnzahl(List<Kunde> kundenListe)
    {
        Console.WriteLine("Anzahl Kunden: " + kundenListe.Count);
    }
}
