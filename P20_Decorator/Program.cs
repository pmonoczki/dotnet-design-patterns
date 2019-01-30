using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P20_Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IFegyver fegyver = new Puska();
            //fegyver.Tüzelés();

            fegyver = new TávcsövesFegyver(new Golyószóró());
            //fegyver.Tüzelés();

            fegyver = new GránátvetősFegyver(new TávcsövesFegyver(new Golyószóró()));
            fegyver.Tüzelés();

            Console.ReadLine();
        }
    }

    //class Fegyver
    //{

    //}

    //class Puska : Fegyver
    //{

    //}

    //class TávcsövesPuska : Puska
    //{

    //}

    //class Gránátvető : Fegyver
    //{

    //}


    //Component
    interface IFegyver
    {
        void Tüzelés();
    }

    //Concrete Component
    class Puska : IFegyver
    {
        public void Tüzelés()
        {
            Console.WriteLine("Messzire lövök.");
        }
    }

    //Concrete Component
    class Golyószóró : IFegyver
    {
        public void Tüzelés()
        {
            Console.WriteLine("Marha gyorsan lövök!");
        }
    }


    //Decorator
    abstract class FejlesztettFegyver : IFegyver
    {
        private IFegyver alapfegyver = null;

        public FejlesztettFegyver(IFegyver alapfegyver)
        {
            if (alapfegyver == null)
            {
                throw new ArgumentNullException();
            }
            this.alapfegyver = alapfegyver;
        }

        public virtual void Tüzelés()
        {
            alapfegyver.Tüzelés();
        }
    }

    //Concrete Decorator
    class TávcsövesFegyver : FejlesztettFegyver
    {
        public TávcsövesFegyver(IFegyver alapfegyver)
            : base(alapfegyver)
        {}

        public void Célzás()
        {
            Console.WriteLine("Célzok... célzok...");
        }

        public override void Tüzelés()
        {
            Célzás();
            base.Tüzelés();
        }
    }

    //Concrete Decorator
    class GránátvetősFegyver : FejlesztettFegyver
    {
        public GránátvetősFegyver(IFegyver alapfegyver)
            : base(alapfegyver)
        {}

        void Gránátvetés()
        {
            Console.WriteLine("ééés még egy gránátot is kap.");
        }

        public override void Tüzelés()
        {
            base.Tüzelés();
            Gránátvetés();
        }
    }

}
