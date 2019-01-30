using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P15_Template
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Add meg, hogy mercedes vagy ifa: ");
            string s = Console.ReadLine();

            Car c = new Car();
            CarCreator cc = null;

            if (s == "mercedes")
            {
                cc = new MercedesCreator();
            }
            else if (s == "ifa")
            {
                cc = new IfaCreator();
            }
            else
            {
                Console.WriteLine("Ilyen kocsit nem tudunk gyártani.");
                Console.Read();
                return;
            }

            cc.CreateCar(c);

            Console.Read();
        }
    }

    class Car
    {
        public string Chassis { get; set; }
        public string Engine { get; set; }
        public string Accessories { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Ez egy {0} motor {1} alvázon a következő kiegészítőkkel: {2}",
                Engine, Chassis, Accessories);
        }
    }

    abstract class CarCreator
    {
        public abstract void CreateEngine(Car c);
        public abstract void CreateChassis(Car c);
        public abstract void CreateAccessories(Car c);

        public void CreateCar(Car c)
        {
            LogEvent("Elkezdődött az alváz gyártása.");
            CreateChassis(c);
            LogEvent("Elkezdődött a motor gyártása.");
            CreateEngine(c);
            LogEvent("Elkezdődött a kiegészítők hozzáadása.");
            CreateAccessories(c);
            LogEvent("Az autó elkészült.");
            Console.WriteLine(c.ToString());
        }

        private void LogEvent(string p)
        {
            Console.WriteLine(p);
        }
    }

    class MercedesCreator : CarCreator
    {

        public override void CreateEngine(Car c)
        {
            c.Engine = "Mercedes motor";
        }

        public override void CreateChassis(Car c)
        {
            c.Chassis = "Mercedes alváz";
        }

        public override void CreateAccessories(Car c)
        {
            c.Accessories = "Kerekek, fék, légkondi.";
        }
    }

    class IfaCreator : CarCreator
    {

        public override void CreateEngine(Car c)
        {
            c.Engine = "Jó hangos motor";
        }

        public override void CreateChassis(Car c)
        {
            c.Chassis = "Kovácsoltvas";
        }

        public override void CreateAccessories(Car c)
        {
            c.Accessories = "Kék festés";
        }
    }

}