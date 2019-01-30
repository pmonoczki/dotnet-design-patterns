using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventHandlerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass mc = new MyClass();
            mc.Esemény += new EventHandler(mc_Esemény);
            mc.Esemény += new EventHandler(mc_Esemény2);
            mc.Elindít();


            Console.Read();
        }

        static void mc_Esemény(object sender, EventArgs e)
        {
            Console.WriteLine("Első");
        }
        static void mc_Esemény2(object sender, EventArgs e)
        {
            ((MyClass)sender).Esemény += new EventHandler(mc_Esemény);
            Console.WriteLine("Második");
        }
    }

    class MyClass
    {
        public event EventHandler Esemény;

        public void Elindít()
        {
            if (Esemény != null)
            {
                Esemény(this, EventArgs.Empty);
            }
        }
    }
}
