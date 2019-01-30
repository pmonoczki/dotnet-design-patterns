using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P03_Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Építkezés építkezés = new LuxusvillaÉpítés();
            JólMegfizetettÉpítkezésIrányító irányító = new JólMegfizetettÉpítkezésIrányító();
            irányító.Építés(építkezés);
            Épület épület = építkezés.Épület;
            épület.Megtekintés();


            Console.Read();
        }
    }

    //Product
    class Épület
    {
        string épülettípus;
        public string Terv { get; set; }
        public bool Engedélyezve { get; set; }
        public DateTime Megépítve { get; set; }
        public DateTime Átadható { get; set; }

        public Épület(string épülettípus)
        {
            this.épülettípus = épülettípus;
        }

        public void Megtekintés()
        {
            Console.WriteLine("Típus:       {0}",
                épülettípus);
            Console.WriteLine("Terv:        {0}",
                Terv); 
            Console.WriteLine("Engedélyezve:{0}",
                 Engedélyezve); 
            Console.WriteLine("Megépítve:   {0}",
                 Megépítve.ToShortDateString()); 
            Console.WriteLine("Átadva:      {0}",
                 Átadható.ToShortDateString());
        }
    }

    //Builder
    abstract class Építkezés
    {
        protected Épület épület;
        public Épület Épület
        {
            get { return épület; }
        }

        public abstract void Tervezés();
        public abstract void Engedélyezés();
        public abstract void Megépítés();
        public abstract void Ellenőrzés();
    }

    //ConcreteBuilder1
    class CsaládiHázÉpítés : Építkezés
    {
        public CsaládiHázÉpítés()
        {
            épület = new Épület("Családi ház");
        }

        public override void Tervezés()
        {
            épület.Terv = "3 szoba, 4 kerék, 7 törpe";
        }

        public override void Engedélyezés()
        {
            épület.Engedélyezve = true;
        }

        public override void Megépítés()
        {
            épület.Megépítve = DateTime.Now;
        }

        public override void Ellenőrzés()
        {
            épület.Átadható = new DateTime(2012, 1, 1);
        }
    }

    //ConcreteBuilder2
    class LuxusvillaÉpítés : Építkezés
    {
        public LuxusvillaÉpítés()
        {
            épület = new Épület("Luxusvilla");
        }

        public override void Tervezés()
        {
            épület.Terv = "9 szoba, 11 wc, 3 garázs";
        }

        public override void Engedélyezés()
        {
            épület.Engedélyezve = true;
        }

        public override void Megépítés()
        {
            épület.Megépítve = DateTime.Now;
        }

        public override void Ellenőrzés()
        {
            épület.Átadható = DateTime.Now;
        }
    }

    //Director
    class ÉpítkezésIrányító
    {
        public void Építés(Építkezés építkezés)
        {
            építkezés.Tervezés();
            építkezés.Engedélyezés();
            építkezés.Megépítés();
            építkezés.Ellenőrzés();
        }
    }

    class JólMegfizetettÉpítkezésIrányító
    {
        public void Építés(Építkezés építkezés)
        {
            építkezés.Tervezés();
            építkezés.Megépítés();
            építkezés.Ellenőrzés();
            építkezés.Engedélyezés();
        }
    }
}