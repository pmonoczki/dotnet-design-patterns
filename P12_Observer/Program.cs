using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P12_Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            Cartman c = new Cartman();
            Alien a1 = new Alien();
            Alien a2 = new Alien();
            Jewsian j = new Jewsian();

            c.Attach(a1);
            c.Attach(a2);
            c.Attach(j);
            c.Pulse = 200;

            Console.ReadLine();
        }
    }

    abstract class Human
    {
        List<IHumanProbeObserver> observers = new List<IHumanProbeObserver>();

        public void Attach(IHumanProbeObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IHumanProbeObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IHumanProbeObserver item in observers)
            {
                item.Update(this);
            }
        }
    }

    interface IHumanProbeObserver
    {
        void Update(Human sender);
    }


    class Cartman : Human
    {
        private int pulse;
        public int Pulse
        {
            get { return pulse; }
            set 
            { 
                pulse = value;
                NotifyObservers();
            }
        }

    }


    class Alien : IHumanProbeObserver
    {

        #region IHumanProbeObserver Members

        public void Update(Human sender)
        {
            if (sender is Cartman)
            {
                Console.WriteLine(
                    "Cartman állapotában változás állt be: pulzus={0}", 
                    ((Cartman)sender).Pulse);
            }
        }

        #endregion
    }

    class Jewsian : IHumanProbeObserver
    {

        #region IHumanProbeObserver Members

        public void Update(Human sender)
        {
            Console.WriteLine("Emelkedik a nézettség.");
        }

        #endregion
    }

}