using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P17_Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Ügyfél ü = new Ügyfél { Név = "Fat Tony", 
                Egyenleg = 500000 };

            Szolgáltatás sz = null;

            Console.WriteLine("[e]mberrablás, [p]énzmosás vagy [r]uhatisztítás?");
            string s = Console.ReadLine();
            if (s == "e")
            {
                sz = new EmberRablás
                {
                    Ügyfél = ü,
                    Kit = "Bart",
                    Céldátum = DateTime.Now,
                    SzolgáltatásDíja = 10000,
                    SzükségEseténKivégzendő = true,
                    Váltságdíj = 20000
                };
            }
            else if (s == "p")
            {
                sz = new PénzMosás
                {
                    Ügyfél = ü,
                    Összeg = 100000,
                    Céldátum = DateTime.Now,
                    FedőCég = "Pizza de la Della",
                    SzolgáltatásDíja = 10000
                };
            }
            else if (s == "r")
            {
                sz = new RuhaTisztításAdapter(
                    new RuhaTisztítás(ü, RuhaTisztításTípus.Alap));
            }

            if (sz != null)
            {
                sz.Kész += new EventHandler<EventArgs>(sz_Kész);
                sz.SzolgáltatásElvégzése();
            }

            Console.ReadLine();
        }

        static void sz_Kész(object sender, EventArgs e)
        {
            Console.WriteLine("Szolgáltatás elvégezve: {0}, {1} részére.",
                sender.GetType().Name, 
                ((Szolgáltatás)sender).Ügyfél.Név);
        }
    }


    public class Ügyfél
    {
        public string Név { get; set; }
        public int Egyenleg { get; set; }
    }

    //Adaptee
    public class RuhaTisztítás
    {
        public Ügyfél Ügyfél { get; set; }
        public RuhaTisztításTípus Tisztítás { get; private set; }
        public int Ár { get; private set; }

        public RuhaTisztítás(Ügyfél ü, RuhaTisztításTípus t)
        {
            Ügyfél = ü;
            Tisztítás = t;
            Árkalkuláció();
        }

        private void Árkalkuláció()
        {
            if (Tisztítás == RuhaTisztításTípus.Alap)
            {
                Ár = 5000;
            }
            else
            {
                Ár = 10000;
            }
        }

        public void TípusVáltás(RuhaTisztításTípus újTípus)
        {
            if (újTípus != Tisztítás)
            {
                Tisztítás = újTípus;
                Árkalkuláció();
            }
        }

        public void MosásIndítása()
        {
            if (Ügyfél.Egyenleg >= Ár)
            {
                Console.WriteLine(
                    "Az {0} mosás {1} részére indul.",
                    Tisztítás, Ügyfél.Név);
                Ügyfél.Egyenleg -= Ár;
                Console.WriteLine("Mosás kész.");
            }
            else
            {
                Console.WriteLine(
                    "Nincs elég pénz a mosáshoz.");
            }
        }

        public void Jelzés()
        {
            //...
            Console.WriteLine("Az ügyfél megkapta a tájékoztatást.");
        }

    }

    public enum RuhaTisztításTípus { Alap, Extra }


    public abstract class Szolgáltatás
    {
        public Ügyfél Ügyfél { get; set; }
        public int SzolgáltatásDíja { get; set; }
        public DateTime Céldátum { get; set; }

        public event EventHandler<EventArgs> Kész;

        public virtual void SzolgáltatásElvégzése()
        {
            if (Kész != null)
            {
                Kész(this, EventArgs.Empty);
            }
        }
    }

    public class PénzMosás : Szolgáltatás
    {
        public int Összeg { get; set; }
        public string FedőCég { get; set; }

        public override void SzolgáltatásElvégzése()
        {
            Console.WriteLine("{0} buznyákot átmostunk a {1} fedőcégen keresztül {2} számára.",
                Összeg, FedőCég, Ügyfél.Név);
            Ügyfél.Egyenleg -= SzolgáltatásDíja;
            Console.WriteLine("{0} levonva az ügyféltől.", 
                SzolgáltatásDíja);
            base.SzolgáltatásElvégzése();
        }
    }

    public class EmberRablás : Szolgáltatás
    {
        public string Kit { get; set; }
        public int Váltságdíj { get; set; }
        public bool SzükségEseténKivégzendő { get; set; }

        public override void SzolgáltatásElvégzése()
        {
            Console.WriteLine("{0}-t elraboltuk, {1} pénzt követelünk érte, ha kell, {2}",
                Kit, Váltságdíj, 
                SzükségEseténKivégzendő ? "eltesszük láb alól" : 
                "... akkor kell.");
            //...
            Ügyfél.Egyenleg -= SzolgáltatásDíja;
            Console.WriteLine("{0} levonva az ügyféltől.", 
                SzolgáltatásDíja);
            base.SzolgáltatásElvégzése();
        }
    }

    public class RuhaTisztításAdapter : Szolgáltatás
    {
        RuhaTisztítás rt = null;

        public RuhaTisztításAdapter(RuhaTisztítás rt)
        {
            Ügyfél = rt.Ügyfél;
            this.rt = rt;
        }

        public int SzolgáltatásDíja
        {
            get { return rt.Ár; }
            set 
            {
                throw new InvalidOperationException(
                    "Ezt a tulajdonságot csak a típuson keresztül lehet megváltoztatni.");
            }
        }

        public RuhaTisztításTípus Típus
        {
            get { return rt.Tisztítás; }
            set { rt.TípusVáltás(value); }
        }

        public override void SzolgáltatásElvégzése()
        {
            rt.MosásIndítása();
            rt.Jelzés();
            base.SzolgáltatásElvégzése();
        }

    }

}