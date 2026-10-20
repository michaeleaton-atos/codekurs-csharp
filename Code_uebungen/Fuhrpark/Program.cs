// Online C# compiler (editor) for free
// Write and run C# online using this editor.

using System;
using System.Collections.Generic;
interface IAufladbar
{
    void Aufladen();
}

interface IVersicherbar
{
    decimal Versicherungswert { get; }
    decimal BerechnePraemie();
}
abstract class Fahrzeug
{

    private string _marke;
    private int _kilometerstand;

    public string Marke
    {
        get { return _marke; }
    }

    public int Kilometerstand
    {
        get { return _kilometerstand; }
    }

    public Fahrzeug(string marke)
    {
        _marke = marke;
        _kilometerstand = 0;
    }

    public void Fahren(int km)
    {
        if (km > 0)
        {
            _kilometerstand += km;
        }
    }

    public abstract void Hupen();

    public virtual string Beschreibung()
    {
        return $"Marke: {Marke}, Kilometerstand: {Kilometerstand}";
    }
}

class Auto : Fahrzeug, IVersicherbar
{
    public string Modell { get; }
    public int Tueren { get; }
    public decimal Versicherungswert { get; }

    public Auto(string marke, string modell, int tueren, decimal versicherungswert)
        : base(marke)
    {
        Modell = modell;
        Tueren = tueren;
        Versicherungswert = versicherungswert;
    }

    public virtual decimal BerechnePraemie()
{
    return Versicherungswert * 0.03m;
}

    public override void Hupen()
    {
        Console.WriteLine("Hup, hup!");
    }

    public override string Beschreibung()
    {
        return base.Beschreibung() +
               $", Modell: {Modell}, Türen: {Tueren}";
    }
}

class Motorrad : Fahrzeug, IVersicherbar
{
    public int Hubraum { get; }
    public decimal Versicherungswert { get; }
   public Motorrad(string marke, int hubraum, decimal versicherungswert)
    : base(marke)
{
    Hubraum = hubraum;
    Versicherungswert = versicherungswert;
}

    public override void Hupen()
    {
        Console.WriteLine("Brumm!");
    }

    public override string Beschreibung()
    {
        return base.Beschreibung() +
               $", Hubraum: {Hubraum}";
    }
    public decimal BerechnePraemie()
{
    return Versicherungswert * 0.05m;
}
}

sealed class Fahrrad : Fahrzeug
{
    public int AnzahlGaenge { get; }

    public Fahrrad(string marke, int anzahlGaenge)
        : base(marke)
    {
        AnzahlGaenge = anzahlGaenge;
    }

    public override void Hupen()
    {
        Console.WriteLine("Kling, kling!");
    }

    public override string Beschreibung()
    {
        return base.Beschreibung() +
               $", Gänge: {AnzahlGaenge}";
    }
}

class ElektroAuto : Auto, IAufladbar
{
    public int AkkuProzent { get; private set; }

 public ElektroAuto(
    string marke,
    string modell,
    int tueren,
    int akkuProzent,
    decimal versicherungswert)
    : base(marke, modell, tueren, versicherungswert)
{
    AkkuProzent = akkuProzent;
}

    public void Aufladen()
    {
        AkkuProzent = 100;
        Console.WriteLine("ElektroAuto wurde aufgeladen.");
    }
    public override decimal BerechnePraemie()
{
    return base.BerechnePraemie() * 0.9m;
}
}

class Smartphone : IAufladbar, IVersicherbar
{
    public int AkkuProzent { get; private set; }
    public decimal Versicherungswert { get; }

    public Smartphone(int akkuProzent, decimal versicherungswert)
    {
        AkkuProzent = akkuProzent;
        Versicherungswert = versicherungswert;
    }

    public void Aufladen()
    {
        AkkuProzent = 100;
        Console.WriteLine("Smartphone wurde aufgeladen.");
    }

    public decimal BerechnePraemie()
    {
        return Versicherungswert * 0.08m;
    }
}


    class Program
{
    static void Main()
    {
        Auto auto = new Auto("BMW", "M3", 4, 50000m);
        Motorrad motorrad = new Motorrad("Yamaha", 700, 12000m);
        Fahrrad fahrrad = new Fahrrad("Cube", 21);
        ElektroAuto elektroAuto = new ElektroAuto("Tesla", "Model 3", 4, 50, 60000m);
        Smartphone smartphone = new Smartphone(40, 1000m);

        List<Fahrzeug> fahrzeuge = new List<Fahrzeug>();
        fahrzeuge.Add(auto);
        fahrzeuge.Add(motorrad);
        fahrzeuge.Add(fahrrad);
        fahrzeuge.Add(elektroAuto);

        List<IAufladbar> aufladbare = new List<IAufladbar>();
        aufladbare.Add(elektroAuto);
        aufladbare.Add(smartphone);

        List<IVersicherbar> versicherbare = new List<IVersicherbar>();
        versicherbare.Add(auto);
        versicherbare.Add(motorrad);
        versicherbare.Add(elektroAuto);
        versicherbare.Add(smartphone);

        foreach (Fahrzeug fahrzeug in fahrzeuge)
        {
            fahrzeug.Hupen();
            Console.WriteLine(fahrzeug.Beschreibung());
        }

        foreach (IAufladbar objekt in aufladbare)
        {
            objekt.Aufladen();
        }

        foreach (IVersicherbar objekt in versicherbare)
        {
            Console.WriteLine(objekt.BerechnePraemie());
        }
    }

}