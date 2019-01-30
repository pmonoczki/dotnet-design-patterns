using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P19_Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Közkatona kk1 = new Közkatona() { Név = "Bruce" };
            Közkatona kk2 = new Közkatona() { Név = "Steve" };
            Golyószórós g1 = new Golyószórós();
            Golyószórós g2 = new Golyószórós();
            Kommandós k = new Kommandós();

            Csapat raj1 = new Csapat();
            raj1.EgységHozzáadása(kk1);
            raj1.EgységHozzáadása(g1);

            Csapat raj2 = new Csapat();
            raj2.EgységHozzáadása(kk2);
            raj2.EgységHozzáadása(g2);

            Csapat zászlóalj = new Csapat();
            zászlóalj.EgységHozzáadása(raj1);
            zászlóalj.EgységHozzáadása(raj2);
            zászlóalj.EgységHozzáadása(k);

            zászlóalj.Ölj();

            Console.ReadLine();
        }
    }

    //Component
    interface IEgység
    {
        void Ölj();
    }

    //Leaf
    class Közkatona : IEgység
    {
        public string Név { get; set; }

        public void Ölj()
        {
            Console.WriteLine(
                "{0} vagyok, és most lelőlek!", Név);
        }
    }

    class Golyószórós : IEgység
    {

        #region IEgység Members

        public void Ölj()
        {
            Console.WriteLine("Most nagyon lelőlek!");
        }

        #endregion
    }

    class Kommandós : IEgység
    {

        #region IEgység Members

        public void Ölj()
        {
            Console.WriteLine("Csendben lelőlek.");
        }

        #endregion
    }

    //Composite
    class Csapat : IEgység
    {
        List<IEgység> egységek = new List<IEgység>();

        public void EgységHozzáadása(IEgység e)
        {
            if (!egységek.Contains(e))
            {
                egységek.Add(e);
            }
        }

        public void EgységLeválasztása(IEgység e)
        {
            egységek.Remove(e);
        }



        #region IEgység Members

        public void Ölj()
        {
            foreach (var item in egységek)
            {
                item.Ölj();
            }
        }

        #endregion
    }
}
