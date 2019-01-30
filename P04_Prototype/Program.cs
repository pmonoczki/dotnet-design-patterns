using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P04_Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            WindwosRendszerkép wrk1 = new WindwosRendszerkép(3, 2);
            wrk1.SzétHekkelés();

            wrk1.Meghajtók.Add(new HálózatiMeghajtó { Név= "C", SzabadHely=20});
            wrk1.Meghajtók.Add(new HálózatiMeghajtó { Név= "D", SzabadHely=10});


            WindwosRendszerkép wrk2 = wrk1.Clone() 
                as WindwosRendszerkép;
            Console.WriteLine(wrk2);
            foreach (var item in wrk2.Meghajtók)
            {
                Console.WriteLine(item.Név);
            }

            Console.Read();
        }
    }

    //Prototype
    abstract class Rendszerkép
    {
        public abstract Rendszerkép Klónozás();
    }

    //ConcretePrototype
    class WindwosRendszerkép : Rendszerkép, ICloneable
    {
        int foglaltHely, memóriaFoglalás, hátsóbejáratok = 20;

        public WindwosRendszerkép(int helyfoglalás, int memóriafoglalás)
        {
            foglaltHely = helyfoglalás;
            memóriaFoglalás = memóriafoglalás;
        }

        public void SzétHekkelés()
        {
            foglaltHely--;
            memóriaFoglalás--;
            hátsóbejáratok--;
        }

        public override string ToString()
        {
            return string.Format(
                "Windows: HDD: {0}, RAM: {1}, Backdoor: {2}",
                foglaltHely, memóriaFoglalás, hátsóbejáratok);
        }

        public override Rendszerkép Klónozás()
        {
            Console.WriteLine("Klónozás: {0}, {1}, {2}",
                foglaltHely, memóriaFoglalás, hátsóbejáratok);
            //WindwosRendszerkép klón = new 
            //    WindwosRendszerkép(foglaltHely, memóriaFoglalás);
            //klón.hátsóbejáratok = this.hátsóbejáratok;

            WindwosRendszerkép klón = this.MemberwiseClone() 
                as WindwosRendszerkép;
            
            return klón;
        }

        public List<HálózatiMeghajtó> Meghajtók = 
            new List<HálózatiMeghajtó>();

        #region ICloneable Members

        public object Clone()
        {
            WindwosRendszerkép klón = new 
                WindwosRendszerkép(foglaltHely, memóriaFoglalás);
            klón.hátsóbejáratok = this.hátsóbejáratok;

            klón.Meghajtók = new List<HálózatiMeghajtó>();
            foreach (var item in this.Meghajtók)
            {
                klón.Meghajtók.Add(item.Clone() as 
                    HálózatiMeghajtó);
            }
            return klón;
        }

        #endregion
    }

    class HálózatiMeghajtó : ICloneable
    {
        public string Név { get; set; }
        public int SzabadHely { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return new HálózatiMeghajtó
            {
                Név = this.Név,
                SzabadHely = this.SzabadHely
            };
        }

        #endregion
    }
}