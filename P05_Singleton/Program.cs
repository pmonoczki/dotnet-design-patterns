using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P05_Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton s1 = Singleton.Instance;
            s1.Név = "alma";
            Singleton s2 = Singleton.Instance;
            Console.WriteLine(s2.Név);

            Console.Read();
        }
    }

    //class Singleton
    //{
    //    public string Név { get; set; }

    //    private Singleton() { }

    //    private static Singleton instance = null;

    //    public static Singleton Instance
    //    {
    //        get 
    //        {
    //            lock (locker)
    //            {
    //                if (instance == null)
    //                {
    //                    instance = new Singleton();
    //                }
    //                return instance; 
    //            }
    //        }
    //    }

    //    static readonly object locker = new object();
    //}


    //class Singleton
    //{
    //    public string Név { get; set; }

    //    private Singleton() { }

    //    private static Singleton instance = 
    //        new Singleton();

    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //                return instance;
    //        }
    //    }

    //}

    class Singleton
    {
        public string Név { get; set; }
        public static Singleton Instance { get; private set; }
        private Singleton() { }
        static Singleton() { Instance = new Singleton(); }

        

    }
}