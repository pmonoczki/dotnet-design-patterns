using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P07a_Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Reciever rec = new Reciever();
            Command<Reciever> cmd = new ConcreteCommand(rec);
            Invoker<Reciever> invoker = new Invoker<Reciever>();
            invoker.SetCommand(cmd);
            invoker.ExecuteCommand();

            Console.Read();
        }
    }

    class Reciever
    {
        public void Action()
        {
            Console.WriteLine("Én itt most dolgozom.");
        }
    }


    abstract class Command<T>
    {
        protected T receiver;
        public Command(T rec)
        {
            receiver = rec;
        }
        public abstract void Execute();
    }

    class ConcreteCommand : Command<Reciever>
    {
        public ConcreteCommand(Reciever r)
            : base(r)
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Ez egy commandon keresztül hívódott.");
            receiver.Action();
        }
    }

    class Invoker<T>
    {
        private Command<T> command;
        public void SetCommand(Command<T> cmd)
        {
            command = cmd;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
}
