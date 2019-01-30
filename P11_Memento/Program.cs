using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace P11_Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Alcoholist barney = new Alcoholist("Barney Gumble");
            Console.WriteLine(barney);

            AlcoholistMementoStore ams = new AlcoholistMementoStore();
            ams.Memento = barney.SaveMemento();

            Thread.Sleep(1000);

            barney.Consumption += 30;
            Console.WriteLine(barney);

            barney.RestoreMemento(ams.Memento);
            Console.WriteLine(barney);

            Console.ReadLine();
        }
    }

    //Originator
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

        public AlcoholistMemento SaveMemento()
        {
            return new AlcoholistMemento(Name,
                Consumption, LastModificationDate);
        }

        public void RestoreMemento(AlcoholistMemento memento)
        {
            Name = memento.Name;
            consumption = memento.Consumption;
            LastModificationDate = memento.LastModified;
        }
    }

    class AlcoholistMemento
    {
        public string Name { get; private set; }
        public int Consumption { get; private set; }
        public DateTime LastModified { get; private set; }

        public AlcoholistMemento(string name, 
            int consumption, DateTime lastmodified)
        {
            Name = name;
            Consumption = consumption;
            LastModified = lastmodified;
        }

    }

    //Caretaker
    class AlcoholistMementoStore
    {
        public AlcoholistMemento Memento { get; set; }
    }
}