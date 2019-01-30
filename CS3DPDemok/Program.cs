using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P01_Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            //Univerzum u = new Univerzum();
            //u.Típus = "star wars";

            //Univerzum.UniverzumFeltöltése(u);
            //régi
            //if (u.Típus == "star wars")
            //{
            //    u.karakterek.Add(new StarWarsKarakter());
            //    u.karakterek.Add(new StarWarsKarakter());
            //    u.karakterek.Add(new StarWarsKarakter());
            //}
            //else if (u.Típus == "family guy")
            //{
            //    u.karakterek.Add(new FamilyGuyKarakter());
            //    u.karakterek.Add(new FamilyGuyKarakter());
            //}

            //u.KarakterekListázása();
            Console.Read();
        }


    }

    //Creator / Product
    abstract class Univerzum
    {
        public List<Karakter> karakterek = 
            new List<Karakter>();

        public void KarakterekListázása()
        {
            foreach (var item in karakterek)
            {
                item.Bemutatkozás();
            }
        }

        public string Típus { get; set; }

        //public static void UniverzumFeltöltése(Univerzum u)
        //{
        //    if (u.Típus == "star wars")
        //    {
        //        u.karakterek.Add(new StarWarsKarakter());
        //        u.karakterek.Add(new StarWarsKarakter());
        //        u.karakterek.Add(new StarWarsKarakter());
        //    }
        //    else if (u.Típus == "family guy")
        //    {
        //        u.karakterek.Add(new FamilyGuyKarakter());
        //        u.karakterek.Add(new FamilyGuyKarakter());
        //    }
        //}

        public abstract void FeltöltElemekkel();
    }

    //ConcreteProduct
    class StarWarsUniverzum : Univerzum
    {
        public override void FeltöltElemekkel()
        {
            this.karakterek.Add(new StarWarsKarakter { Név = "Luke" });
            this.karakterek.Add(new StarWarsKarakter { Név = "Leia" });
        }
    }

    //ConcreteProduct
    class FamilyGuyUniverzum : Univerzum
    {
        public override void FeltöltElemekkel()
        {
            this.karakterek.Add(new FamilyGuyKarakter { Név = "Peter" });
            this.karakterek.Add(new FamilyGuyKarakter { Név = "Stewie" });
        }
    }


    //Creator (Factory)
    sealed class UniverzumKészítő
    {
        public Dictionary<Type, bool> engedélyek { get; set; }

        public UniverzumKészítő()
        {
            engedélyek = new Dictionary<Type, bool>();
            engedélyek.Add(typeof(StarWarsKarakter), false);
            engedélyek.Add(typeof(FamilyGuyKarakter), false);
        }

        public void Engedélyezés<T>() 
            where T: Karakter
        {
            engedélyek[typeof(T)] = true;
        }

        public void UniverzumKészítés(Univerzum u)
        {
            //if (u.Típus == "star wars")
            //{
            //    if (engedélyek[typeof(StarWarsKarakter)])
            //    {
            //        u.karakterek.Add(new StarWarsKarakter());
            //        u.karakterek.Add(new StarWarsKarakter());
            //        u.karakterek.Add(new StarWarsKarakter());
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException(
            //            "Ez a típus nem engedélyezett.");
            //    }
            //}
            //else if (u.Típus == "family guy")
            //{
            //    if (engedélyek[typeof(FamilyGuyKarakter)])
            //    {
            //        u.karakterek.Add(new FamilyGuyKarakter());
            //        u.karakterek.Add(new FamilyGuyKarakter());
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException(
            //            "Ez a típus nem engedélyezett.");
            //    }
            //}


            if (u is StarWarsUniverzum && engedélyek[typeof(StarWarsKarakter)])
            {
                u.karakterek.Add(new StarWarsKarakter { Név = "Luke" });
                u.karakterek.Add(new StarWarsKarakter { Név = "Leia" });
            }
            else if (u is FamilyGuyUniverzum && engedélyek[typeof(FamilyGuyKarakter)])
            {
                u.karakterek.Add(new FamilyGuyKarakter { Név = "Peter" });
                u.karakterek.Add(new FamilyGuyKarakter { Név = "Stewie" });
            }
        }

        public Univerzum UniverzumKészítés<T>()
            where T : Univerzum, new()
        {
            T u = new T();

            if (u is StarWarsUniverzum && engedélyek[typeof(StarWarsKarakter)])
            {
                u.karakterek.Add(new StarWarsKarakter { Név = "Luke" });
                u.karakterek.Add(new StarWarsKarakter { Név = "Leia" });
            }
            else if (u is FamilyGuyUniverzum && engedélyek[typeof(FamilyGuyKarakter)])
            {
                u.karakterek.Add(new FamilyGuyKarakter { Név = "Peter" });
                u.karakterek.Add(new FamilyGuyKarakter { Név = "Stewie" });
            }

            return u;
        }
    }

    //Product
    abstract class Karakter
    {
        public string Név { get; set; }

        public void Bemutatkozás()
        {
            Console.WriteLine(
                "Szia, én {0} vagyok.", Név);
        }
    }

    //ConcreteProduct
    class FamilyGuyKarakter : Karakter
    {
        public FamilyGuyKarakter()
        {
            Név = "Family Guy Karakter";
        }
    }

    //ConcreteProduct
    class StarWarsKarakter : Karakter
    {
        public StarWarsKarakter()
        {
            Név = "Star Wars Karakter";
        }
    }

}
