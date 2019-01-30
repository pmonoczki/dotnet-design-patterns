using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P18_Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            MobilTelefonHardver mh = new NormálMobilHardver(20, 2);
            IPhone ip = new IPhone(mh);
            ip.Telefonálás(1234567);
            Console.WriteLine();

            DiePhone dp = new DiePhone(mh);
            dp.Telefonálás(1234567);
            Console.WriteLine();

            mh = new KínaiMobilHardver(10, 3);
            ip = new IPhone(mh);
            ip.Telefonálás(1234567);

            Console.ReadLine();



            Console.ReadLine();
        }
    }

    class SzokolRádió
    {
        public void Állomáskeresés()
        {
            Console.WriteLine("Állomást keresek");
        }

        public void Vétel()
        {
            Console.WriteLine("Veszem az adást...");
        }
    }

    //Abstraction
    class MobilTelefon
    {
        private MobilTelefonHardver hardver = null;

        public MobilTelefon(MobilTelefonHardver hardver)
        {
            this.hardver = hardver;
        }

        public virtual void Telefonálás(int szám)
        {
            hardver.ProgramokFagyasztása();
            hardver.TelefonálásIndítása(szám);
            hardver.ProgramokFolytatása();
        }
    }

    //Implementor
    abstract class MobilTelefonHardver
    {
        public int VételiErősség { get; set; }
        public int Sugárzás { get; set; }

        public MobilTelefonHardver(
            int jelerősség, int sugárzás)
        {
            VételiErősség = jelerősség;
            Sugárzás = sugárzás;
        }

        public abstract void ProgramokFagyasztása();
        public abstract void TelefonálásIndítása(int szám);
        public abstract void ProgramokFolytatása();
    }


    //RefinedAbstraction
    class IPhone : MobilTelefon
    {
        public IPhone(MobilTelefonHardver hw)
            : base(hw)
        {

        }

        public override void Telefonálás(int szám)
        {
            Console.WriteLine("IPhone telefonál hazaaaa");
            base.Telefonálás(szám);
            Console.WriteLine("IPhone lerakva");
        }
    }

    //RefinedAbstraction
    class DiePhone : MobilTelefon
    {
        public DiePhone(MobilTelefonHardver hw)
            : base(hw)
        { }

        public override void Telefonálás(int szám)
        {
            Console.WriteLine("Ne tarts túl közel a fejedhez.");
            base.Telefonálás(szám);
        }
    }


    //Conrete Implementor
    class NormálMobilHardver : MobilTelefonHardver
    {
        public NormálMobilHardver(int jelerősség, 
            int sugárzás)
            : base(jelerősség, sugárzás)
        {}

        public override void ProgramokFagyasztása()
        {
            Console.WriteLine("Programok fagyasztva.");
        }

        public override void TelefonálásIndítása(int szám)
        {
            if (VételiErősség > 10)
            {
                Console.WriteLine("Nő a számlád");
            }
            else
            {
                Console.WriteLine("Ez ma nem fog összejönni.");
            }
        }

        public override void ProgramokFolytatása()
        {
            Console.WriteLine("A programok újra futnak.");
        }
    }

    //Concrete Implementor
    class KínaiMobilHardver : MobilTelefonHardver
    {
        public KínaiMobilHardver(int jelerősség, int sugárzás)
            : base(jelerősség, sugárzás)
        { }

        SzokolRádió rádió = new SzokolRádió();

        public override void ProgramokFagyasztása()
        {
            Console.WriteLine("Többé-kevésbé lefagyasztva.");
        }

        public override void TelefonálásIndítása(int szám)
        {
            rádió.Állomáskeresés();
            rádió.Vétel();
            if (VételiErősség > 15)
            {
                Console.WriteLine("Most megy tele az agyad sugarakkal.");
            }
            else
            {
                Console.WriteLine("Próbáld máskor.");
            }
        }

        public override void ProgramokFolytatása()
        {
            Console.WriteLine("Megpróbálom, de nem ígérek semmit.");
        }
    }
}
