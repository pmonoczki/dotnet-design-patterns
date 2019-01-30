using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P10_Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Boss joli = new Boss();
            Employee béla = new Employee("Béla");
            joli.Register(béla);
            Employee rashid = new Tester("Rashid");
            joli.Register(rashid);
            Developer ödön = new Developer("Ödön");
            joli.Register(ödön);

            béla.Send("Ödön", "Futtasd le a teszteket");
            ödön.Send("Rashid", "please fix the bugs you made last year");
            rashid.Send("Ödön", "Sori, i dount speeke turkisch");
            ödön.Send("Béla", "Kilépek.");


            Console.ReadLine();
        }
    }

    interface ISupervisor
    {
        void Register(Employee e);
        void Send(string from, string to, string message);
    }

    class Employee
    {
        public string Name { get; protected set; }
        public ISupervisor Supervisor { get; set; }

        public Employee(string name)
        {
            Name = name;
        }

        public void Send(string to, string message)
        {
            Supervisor.Send(Name, to, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine(
                "{0} azt üzente nekem, hogy {1}",
                from, message);
        }
    }

    class Developer : Employee
    {
        public Developer(string name)
            : base(name)
        { }

        public override void Receive(string from, string message)
        {
            Console.WriteLine("Egy fejlesztőnek üzenik:");
            base.Receive(from, message);
        }
    }

    class Tester : Employee
    {
        public Tester(string name)
            : base(name)
        {}

        public override void Receive(string from, string message)
        {
            Console.WriteLine("Egy tesztelőnek üzenik:");
            base.Receive(from, message);
        }
    }


    class Boss : ISupervisor
    {
        Dictionary<string, Employee> emberek =
            new Dictionary<string, Employee>();

        List<string> log = new List<string>();

        #region ISupervisor Members

        public void Register(Employee e)
        {
            if (!emberek.ContainsValue(e))
            {
                emberek.Add(e.Name, e);
                e.Supervisor = this;
            }
        }

        public void Send(string from, string to, string message)
        {
            Employee e = emberek[to];
            if (e != null)
            {
                log.Add(message);
                e.Receive(from, message);
            }
        }

        #endregion

        public void ShowLog()
        {
            foreach (var item in log)
            {
                Console.WriteLine(item);
            }
        }
    }
}