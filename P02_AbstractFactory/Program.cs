using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Erőmű e = new Erőmű(new AmerikaiErőműépítő());
            e.Indítás();

            Erőmű e2 = new Erőmű(new KínaiErőműÉpítő());
            e2.Indítás();

            Console.Read();
        }
    }

    //class AmerikaiErőműÉpítő
    //{
    //    public void TerületÁtvizsgálása() { }
    //    public void BiztonságiEllenőrzés() { }
    //    public AmerikaiVezérlőpult VezérlőpultBeépítése() { }
    //    public AtomReaktor ReaktorBeépítése() { }
    //    public void CsúszópénzekKiosztása() { }
    //}

    //class KínaiErőműÉpítő
    //{
    //    public override Vezérlőpult VezérlőpultLétrehozás() { }
    //    public override Reaktor ReaktorLétrehozása() { }
    //    public void MunkásokVéletlenBalesetei() { }
    //}

    //Abstract factory
    abstract class ErőműÉpítő
    {
        public abstract Vezérlőpult VezérlőpultLétrehozása();
        public abstract Reaktor ReaktorLétrehozása();
    }

    //AbstractProductA
    abstract class Vezérlőpult
    {
        public abstract void Vezérel(Reaktor r);
    }

    //AbstractProductB
    abstract class Reaktor
    {

    }

    //ConcreteProductA1
    class KínaiVezérlőpult : Vezérlőpult
    {
        public override void Vezérel(Reaktor r)
        {
            Console.WriteLine(
                "Találtam egy {0}-t, mit csináljak?", 
                r.GetType().Name);
        }
    }

    //ConcreteProductA2
    class AmerikaiVezérlőpult : Vezérlőpult
    {
        public override void Vezérel(Reaktor r)
        {
            Console.WriteLine(
                "Ezt a {0}-t jól beszabályozom.",
                r.GetType().Name);
        }
    }

    //ProductB1
    class AtomReaktor : Reaktor
    {

    }

    //ProductB2
    class Hőreaktor : Reaktor
    {

    }

    //Client
    class Erőmű
    {
        private Vezérlőpult vezpult;
        private Reaktor reaktor;

        public Erőmű(ErőműÉpítő építő)
        {
            reaktor = építő.ReaktorLétrehozása();
            vezpult = építő.VezérlőpultLétrehozása();
        }

        public void Indítás()
        {
            vezpult.Vezérel(this.reaktor);
        }
    }

    class AmerikaiErőműépítő : ErőműÉpítő
    {
        public override Reaktor ReaktorLétrehozása()
        {
            CsúszópénzekKiosztása();
            return new AtomReaktor();
        }

        private void CsúszópénzekKiosztása()
        {
            Console.WriteLine("Mindenkit lefizetünk.");
        }

        public override Vezérlőpult VezérlőpultLétrehozása()
        {
            TerületÁtvizsgálása();
            BiztonságiEllenőrzés();
            return new AmerikaiVezérlőpult();
        }

        private void BiztonságiEllenőrzés()
        {
            Console.WriteLine("Szerintem minden jó.");
        }

        private void TerületÁtvizsgálása()
        {
            Console.WriteLine("Nem mocsár.");
        }
    }

    //ConcreteFactory2
    class KínaiErőműÉpítő : ErőműÉpítő
    {

        public override Vezérlőpult VezérlőpultLétrehozása()
        {
            return new KínaiVezérlőpult();
        }

        public override Reaktor ReaktorLétrehozása()
        {
            return new Hőreaktor();
        }
    }
}
