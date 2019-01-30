using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P11_SaferMemento
{
    class Program
    {
        static void Main(string[] args)
        {
            Alcoholist barney = new Alcoholist("Barney");

            AlcoholistMementoStore.SaveMemento(barney);
            Console.WriteLine(barney);

            barney.Consumption += 30;

            System.Threading.Thread.Sleep(500);

            Console.WriteLine(barney);

            AlcoholistMementoStore.RestoreMemento(barney);
            Console.WriteLine(barney);


            Console.ReadLine();
        }
    }

    class Alcoholist
    {
        public string Name { get; set; }
        private int consumption;

        public int Consumption
        {
            get { return consumption; }
            set
            {
                LastModificationDate = DateTime.Now;
                consumption = value;
            }
        }

        private DateTime LastModificationDate = DateTime.Now;

        public Alcoholist(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}L @ {2}",
                Name, Consumption,
                LastModificationDate.Ticks);
        }


        internal class AlcoholistMemento
        {
            string name;
            int consumption;
            DateTime lastmodified;
            Alcoholist originator;

            internal AlcoholistMemento(
                Alcoholist originator)
            {
                name = originator.Name;
                consumption = originator.consumption;
                lastmodified = originator.LastModificationDate;
                this.originator = originator;
            }

            internal void RestoreMemento(Alcoholist alc)
            {
                if (!alc.Equals(originator))
                {
                    throw new InvalidOperationException
                        ("Invalid originator.");
                }
                alc.Name = name;
                alc.consumption = consumption;
                alc.LastModificationDate = lastmodified;
            }

            internal static AlcoholistMemento SaveMemento(Alcoholist alc)
            {
                return new AlcoholistMemento(alc);
            }
        }
    }

    //caretaker
    class AlcoholistMementoStore
    {
        static Dictionary<Alcoholist, Alcoholist.AlcoholistMemento> Mementos =
            new Dictionary<Alcoholist, Alcoholist.AlcoholistMemento>();

        public static void SaveMemento(Alcoholist originator)
        {
            if (Mementos.ContainsKey(originator))
            {
                Mementos[originator] = Alcoholist.AlcoholistMemento.SaveMemento(originator);
            }
            else
            {
                Mementos.Add(originator, 
                    Alcoholist.AlcoholistMemento
                    .SaveMemento(originator));
            }
        }

        public static void RestoreMemento(Alcoholist originator)
        {
            if (!Mementos.ContainsKey(originator))
            {
                throw new InvalidOperationException("No memento.");
            }
            Mementos[originator].RestoreMemento(originator);
        }
    }

}