using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P22_Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            SztárGyár gyár = new SztárGyár();
            Celeb villalakó = gyár.CelebIgénylése(SztárTípus.VillaLakó);
            villalakó.Előad("valami hülyeség");

            Celeb villalakó2 = gyár.CelebIgénylése(SztárTípus.VillaLakó);
            villalakó2.Előad("valami más hülyeség");

            Celeb megasztár = gyár.CelebIgénylése(SztárTípus.Megasztár);
            megasztár.Előad("Valami dal");

            Console.Read();
        }
    }

    enum SztárTípus
    {
        VillaLakó, Megasztár, Színész, CelebIsmerős
    }


    //Flyweight
    abstract class Sztár
    {
        public SztárTípus Típus { get; protected set; }
        public bool Férfi { get; protected set; }
        public int Magasság { get; protected set; }
        //...

        public abstract void Előad(string téma);
    }

    //Concrete Flyweight
    class Celeb : Sztár
    {
        public Celeb(SztárTípus típus, 
            bool férfi, int magasság)
        {
            Típus = típus;
            Férfi = férfi;
            Magasság = magasság;
        }

        public override void Előad(string téma)
        {
            Console.WriteLine(
                "Én, mint {0} típusú sztár előadom a következőt: {1}",
                Típus, téma);
        }
    }

    //Flyweight factory
    class SztárGyár
    {
        Dictionary<SztárTípus, Celeb> sztárok =
            new Dictionary<SztárTípus, Celeb>();

        public Celeb CelebIgénylése(SztárTípus t)
        {
            if (!sztárok.ContainsKey(t))
            {
                switch (t)
                {
                    case SztárTípus.VillaLakó:
                        sztárok.Add(t, new Celeb(t, true, 180));
                        break;
                    case SztárTípus.Megasztár:
                        sztárok.Add(t, new Celeb(t, false, 170));
                        break;
                    case SztárTípus.Színész:
                        sztárok.Add(t, new Celeb(t, true, 160));
                        break;
                    case SztárTípus.CelebIsmerős:
                        sztárok.Add(t, new Celeb(t, false, 156));
                        break;
                }
                Console.WriteLine("Új celeb készült: " + t);
            }
            return sztárok[t];
        }
    }
}