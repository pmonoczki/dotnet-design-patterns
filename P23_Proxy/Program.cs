using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P23_Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Ember egyszerűEmber = new Ember();
            Ember unalmasEmber = new Ember { Unalmas = true };
            //egyszerűEmber.Beszélget(unalmasEmber);
            //egyszerűEmber.Beszélget(unalmasEmber);

            Humanoid ember = new Surrogate(new Ember());
            ember.Beszélget(egyszerűEmber);
            ember.Beszélget(unalmasEmber);
            ember.Beszélget(unalmasEmber);

            Console.Read();
        }
    }

    //Subject
    abstract class Humanoid
    {
        public bool Éber { get; protected set; }
        public bool Unalmas { get; set; }

        public abstract void Beszélget(Humanoid h);
    }

    //RealSubject
    class Ember : Humanoid
    {
        public Ember()
        {
            Éber = true;
        }

        public override void Beszélget(Humanoid h)
        {
            if (!Éber)
            {
                throw new InvalidOperationException("<Horkol>");
            }
            Console.WriteLine("Hello, hogy vagy?");
            if (h.Unalmas)
            {
                Éber = false;
            }
        }
    }

    class Surrogate : Humanoid
    {
        Ember ember = null;

        public Surrogate(Ember valódi)
        {
            if (valódi != null)
            {
                ember = valódi;
            }
        }

        public override void Beszélget(Humanoid h)
        {
            if (ember.Éber)
            {
                ember.Beszélget(h);
            }
            else
            {
                Console.WriteLine("Maradj csendben, alszik...");
            }
        }
    }
}