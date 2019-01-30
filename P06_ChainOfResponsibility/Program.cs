using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P06_ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            GéppisztolyosCT gct = new GéppisztolyosCT();
            GéppisztolyosCT gct2 = new GéppisztolyosCT { Következő = gct };
            VillanóCT vct = new VillanóCT { Következő = gct2 };
            AjtótörőCT act = new AjtótörőCT { Következő = vct };
            act.Futtatás();
            Console.Read();
        }
    }

    //Handler
    abstract class AntiTerrorista
    {
        public AntiTerrorista Következő = null;

        protected abstract void Dolgozik();

        public void Futtatás()
        {
            Dolgozik();
            if (Következő != null)
            {
                Következő.Futtatás();
            }
        }

    }
    //ConcreteHandler1
    class AjtótörőCT : AntiTerrorista
    {
        protected override void Dolgozik()
        {
            Console.WriteLine("Puff!");
        }
    }

    class VillanóCT : AntiTerrorista
    {
        protected override void Dolgozik()
        {
            Console.WriteLine("BANG!");
        }
    }

    class GéppisztolyosCT : AntiTerrorista
    {
        protected override void Dolgozik()
        {
            Console.WriteLine("Ratatata!");
        }
    }

}
