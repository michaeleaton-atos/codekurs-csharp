using System;
using System.Collections.Generic;

public interface IAufladbar
{
    void Aufladen();
}

public abstract class Fahrzeug
{
    private string _marke;

    public void Warten()
    {
        Console.WriteLine("Wird gewartet...");
    }

    public string Marke
    {
        get
        {
            return _marke;
        }
    }

    public Fahrzeug(string marke)
    {
        _marke = marke;
    }

    public abstract void Hupen();

    public static void Main(string[] args)
    {
        IAufladbar meinAuto = new ElektroAuto("Tesla");
        IAufladbar meinHandy = new Smartphone();

        meinAuto.Aufladen();
        meinHandy.Aufladen();

        List<Fahrzeug> meineFahrzeuge = new List<Fahrzeug>();

        meineFahrzeuge.Add(new Auto("Ford", "Mustang"));
        meineFahrzeuge.Add(new Motorrad("Honda", 650));

        foreach (Fahrzeug f in meineFahrzeuge)
        {
            f.Hupen();
            f.Warten();
        }
    }

    class Auto : Fahrzeug
    {
        private string _modell;

        public string Modell
        {
            get
            {
                return _modell;
            }
        }

        public override void Hupen()
        {
            Console.WriteLine("Auto hupt");
        }

        public Auto(string marke, string modell)
            : base(marke)
        {
            _modell = modell;
        }
    }

    public class Motorrad : Fahrzeug
    {
        private int _hubraum;

        public override void Hupen()
        {
            Console.WriteLine("Motorrad hupt");
        }

        public int Hubraum
        {
            get
            {
                return _hubraum;
            }
        }

        public Motorrad(string marke, int hubraum)
            : base(marke)
        {
            _hubraum = hubraum;
        }
    }

    public class Fahrrad : Fahrzeug
    {
        public Fahrrad(string marke)
            : base(marke)
        {
        }

        public override void Hupen()
        {
            Console.WriteLine("Kling, kling!");
        }
    }

    class ElektroAuto : Fahrzeug, IAufladbar
    {
        public ElektroAuto(string marke)
            : base(marke)
        {
        }

        public override void Hupen()
        {
            Console.WriteLine("Elektroauto hupt");
        }

        public void Aufladen()
        {
            Console.WriteLine("Elektroauto wird aufgeladen");
        }
    }

    class Smartphone : IAufladbar
    {
        public void Aufladen()
        {
            Console.WriteLine("Smartphone wird aufgeladen");
        }
    }
}
