using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P07b_Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver rec = new Receiver();
            Invoker inv = new Invoker(rec);

            inv.Do(7);
            inv.Do(10);

            inv.Undo();
            Console.WriteLine(rec.State);

            inv.Undo();
            Console.WriteLine(rec.State);

            Console.Read();
        }
    }

    class Receiver
    {
        public int State { get; set; }
        public void Action()
        {
            Console.WriteLine("State: " + State);
        }
    }

    abstract class Command<T, U>
    {
        protected T receiver;
        protected U data;

        public Command(T rec, U data)
        {
            receiver = rec;
            this.data = data;
        }

        public abstract void Execute();
        public abstract void Undo();
    }

    interface ICommand
    {
        void Execute();
        void Undo();
    }

    class ConcreteCommand : ICommand //Command<Receiver, int>
    {
        private Receiver receiver = null;
        private int data;
        public ConcreteCommand(Receiver r, int data)
            //: base(r, data)
        {
            receiver = r;
            this.data = data;
        }

        public void Execute()
        {
            receiver.State += data;
            receiver.Action();
        }

        public void Undo()
        {
            receiver.State -= data;
        }
    }

    class Invoker
    {
        private Receiver receiver = null;
        private List<ICommand> cmds =
            new List<ICommand>();

        public Invoker(Receiver r)
        {
            receiver = r;
        }

        public void Do(int data)
        {
            ConcreteCommand cmd = new ConcreteCommand(receiver, data);
            cmds.Add(cmd);
            cmd.Execute();
        }

        public void Undo()
        {
            if (cmds.Count > 0)
            {
                cmds.Last().Undo();
                cmds.RemoveAt(cmds.Count - 1);
            }
        }
    }
}