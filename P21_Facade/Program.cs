using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P21_Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            Oktatásügy oktatás = new Oktatásügy { Tanárok = 100 };
            Egészségügy egügy = new Egészségügy(100000);
            Sörügy sör = new Sörügy(10000, 10, 100);

            Állam állam = new Állam(oktatás, egügy, sör);
            állam.SörgyárakatKellNyitni(1000);


            Console.ReadLine();
        }
    }

    class Oktatásügy
    {
        public int Tanárok { get; set; }
        public const int EgységFizetés = 100;

        public int ElérhetőErőforrások 
        {
            get { return Tanárok * EgységFizetés; }
        }

        public int Pénzkivonás(int kivontPénz)
        {
            if (kivontPénz > ElérhetőErőforrások)
            {
                throw new InvalidOperationException();
            }

            Tanárok -= (int)Math.Ceiling((double)kivontPénz / EgységFizetés);
            return kivontPénz;
        }
    }

    class Egészségügy
    {
        public int Pénz { get; private set; }
        public bool Sztrájk { get; set; }
        int minimálisPénz = 1000;

        public Egészségügy(int pénz)
        {
            Pénz = pénz;
        }

        public int SztrájkNélkülElvonhatóPénz()
        {
            return Pénz - minimálisPénz;
        }

        public int Elvonás(int elvonandóPénz)
        {
            if (elvonandóPénz > SztrájkNélkülElvonhatóPénz())
            {
                throw new InvalidOperationException();
            }
            else
            {
                Pénz -= elvonandóPénz;
                return elvonandóPénz;
            }
        }
    }

    class Sörügy
    {
        public int Tőke { get; private set; }
        public int Gyárak { get; private set; }
        public int FenntartásiKöltség { get; private set; }

        public Sörügy(int tőke, int gyárak, int fenntartásiktg)
        {
            Tőke = tőke;
            Gyárak = gyárak;
            FenntartásiKöltség = fenntartásiktg;
        }

        public void Gyárnyitás(int újTőke)
        {
            Console.WriteLine(
                "Ez a pénz ({0}) {1} gyár nyitására elég.", 
                újTőke, (int)Math.Truncate((double)újTőke / FenntartásiKöltség));
            Tőke += újTőke;
            Gyárak += újTőke / FenntartásiKöltség;
        }

    }

    //Facade
    class Állam
    {
        Oktatásügy oktatás;
        Egészségügy egügy;
        Sörügy sör;

        public Állam(Oktatásügy oü, Egészségügy eü, Sörügy sü)
        {
            oktatás = oü;
            egügy = eü;
            sör = sü;
        }

        public void SörgyárakatKellNyitni(int db)
        {
            int szüksPénz = db * sör.FenntartásiKöltség;
            int egügySzabadPénz = egügy.SztrájkNélkülElvonhatóPénz();
            int oktatásSzabadPénz = oktatás.ElérhetőErőforrások;

            if (egügySzabadPénz + oktatásSzabadPénz < szüksPénz)
            {
                Console.WriteLine("Ennyi sörgyárra nincs pénz!!!");
                return;
            }
            int szerzettPénz = 0;

            szerzettPénz = egügy.Elvonás(Math.Min
                (egügySzabadPénz, szüksPénz));
            Console.WriteLine("Elvont pénz (egügy): " + szerzettPénz);
            if (szerzettPénz < szüksPénz)
            {
                szerzettPénz += oktatás.Pénzkivonás
                    (szüksPénz - szerzettPénz);
                Console.WriteLine("Elvont pénz (egügy + oktatás): " 
                    + szerzettPénz);
            }

            sör.Gyárnyitás(szerzettPénz);
        }

    }
}