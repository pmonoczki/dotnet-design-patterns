using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P16_Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Parlament parlament = new Parlament();
            parlament.Attach(new Képviselő());
            parlament.Attach(new Polgármester());
            parlament.Attach(new Miniszter());

            parlament.RunVisitor(new KonzolVisitor());
            parlament.RunVisitor(new PolitikusiFizetésVisitor());
            parlament.RunVisitor(new KonzolVisitor());
            parlament.RunVisitor(new PolitikusiPártállásVisitor());
            parlament.RunVisitor(new KonzolVisitor());

            Console.Read();
        }
    }

    interface IElement
    {
        void Accept(IVisitor visitor);
    }

    interface IVisitor
    {
        void Visit(IElement element);
    }

    class Politikus : IElement
    {
        public string Név { get; set; }
        public string Párt { get; set; }
        public int Fizetés { get; set; }
        public int EgyébJuttatás { get; set; }

        public Politikus(string név, string párt, 
            int fizetés, int egyébJuttatás)
        {
            Név = név;
            Párt = párt;
            Fizetés = fizetés;
            EgyébJuttatás = egyébJuttatás;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}+{3}",
                Név, Párt, Fizetés, EgyébJuttatás);
        }

        #region IElement Members

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }

    class Képviselő : Politikus
    {
        public Képviselő()
            : base("Jóságos József", "Rossz Párt", 
            500000, 100000)
        { }
    }

    class Polgármester : Politikus
    {
        public Polgármester()
            : base("Önzetlen Ödön", "Jó Párt", 400000, 150000)
        {}
    }

    class Miniszter : Politikus
    {
        public Miniszter()
            : base("Igazmondó Imre", "Jó Párt", 1000000, 200000)
        {

        }
    }


    class PolitikusiFizetésVisitor : IVisitor
    {

        #region IVisitor Members

        public void Visit(IElement element)
        {
            Politikus p = element as Politikus;
            if (p != null)
            {
                p.Fizetés -= 100000;
                if (p.Párt == "Jó Párt")
                {
                    p.EgyébJuttatás += 200000;
                }
            }
        }

        #endregion
    }

    class PolitikusiPártállásVisitor : IVisitor
    {

        #region IVisitor Members

        public void Visit(IElement element)
        {
            Politikus p = element as Politikus;
            if (p != null)
            {
                if (p.Párt != "Rossz Párt")
                {
                    p.Párt = "Rossz Párt";
                    p.Fizetés -= 50000;
                }
                else
                {
                    p.Párt = "Jó Párt";
                    p.Fizetés += 100000;
                }
            }
        }

        #endregion
    }

    class KonzolVisitor : IVisitor
    {

        #region IVisitor Members

        public void Visit(IElement element)
        {
            Console.WriteLine(element);
        }

        #endregion
    }


    class Parlament
    {
        List<Politikus> politikusok = new List<Politikus>();

        public void Attach(Politikus p)
        {
            politikusok.Add(p);
        }

        public void Detach(Politikus p)
        {
            politikusok.Remove(p);
        }

        public void RunVisitor(IVisitor visitor)
        {
            foreach (var item in politikusok)
            {
                item.Accept(visitor);
            }
            Console.WriteLine();
        }
    }




    //----------------------
    interface IVisitor2
    {
        void Visit(Polgármester p);
        void Visit(Miniszter m);
        void Visit(Képviselő k);
    }

    class FizetésVisitor2 : IVisitor2
    {

        #region IVisitor2 Members
        
        public void Visit(Polgármester p)
        {
            p.Fizetés -= 100000;
        }

        public void Visit(Miniszter m)
        {
            m.Fizetés += 50000;
        }

        public void Visit(Képviselő k)
        {
            k.Fizetés -= 50000;
        }

        #endregion
    }

}