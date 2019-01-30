using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P13_State
{
    class Program
    {
        static void Main(string[] args)
        {
            TestManager tm = new TestManager();
            Console.WriteLine(tm.GetTestState());

            tm.Percentage = 51;
            Console.WriteLine(tm.GetTestState());
            tm.Percentage = 90;
            Console.WriteLine(tm.GetTestState());
            tm.Percentage = 49;
            Console.WriteLine(tm.GetTestState());

            Console.ReadLine();
        }
    }


    public class TestManager
    {
        //private int percentage;
        public int Percentage
        {
            get { return State.Percentage; }
            set { State.SetPercentage(value); }
        }

        public ManagerState State { get; set; }

        public TestManager()
        {
            State = new RedState(this, 0);
        }

        public TestState GetTestState()
        {
            return State.GetTestState();

            //if (Percentage > 0 && percentage < 51)
            //{
            //    HatalmasBajVan();
            //    return TestState.Red;
            //}
            //else if (Percentage > 50 && Percentage < 81)
            //{
            //    return TestState.Yellow;
            //}
            //else
            //{
            //    Fizetésemelést();
            //    return TestState.Green;
            //}
        }

        public void Fizetésemelést()
        {
            Console.WriteLine("Fizetésemelést kérünk!");
        }

        public void HatalmasBajVan()
        {
            Console.WriteLine(
                "Másra outsource-oljuk a melót.");
        }

    }

    public enum TestState
    {
        Green, Yellow, Red
    }


    public abstract class ManagerState
    {
        protected TestManager context = null;
        public int Percentage { get; protected set; }


        public ManagerState(TestManager manager, 
            int percentage)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            context = manager;
            Percentage = percentage;
        }

        public abstract TestState GetTestState();

        public abstract void CheckState();

        public virtual void SetPercentage(int percentage)
        {
            Percentage = percentage;
            CheckState();
        }
    }

    class RedState : ManagerState
    {
        public RedState(TestManager manager, int percentage)
            : base(manager, percentage)
        {}

        public override TestState GetTestState()
        {
            context.HatalmasBajVan();
            return TestState.Red;
        }

        public override void CheckState()
        {
            if (Percentage > 80)
            {
                context.State = new GreenState(context, Percentage);
            }
            else if (Percentage > 50)
            {
                context.State = new YellowState(context, Percentage);
            }
        }
    }

    public class GreenState : ManagerState
    {
        public GreenState(TestManager manager, int percentage)
            : base(manager, percentage)
        {}

        public override TestState GetTestState()
        {
            context.Fizetésemelést();
            return TestState.Green;
        }

        public override void CheckState()
        {
            if (Percentage < 50)
            {
                context.State = new RedState(context, Percentage);
            }
            else if (Percentage < 80)
            {
                context.State = new YellowState(context, Percentage);
            }
        }
    }

    public class YellowState : ManagerState
    {
        public YellowState(TestManager manager, int percentage)
            : base(manager, percentage)
        {}

        public override TestState GetTestState()
        {
            return TestState.Yellow;
        }

        public override void CheckState()
        {
            if (Percentage < 50)
            {
                context.State = new RedState(context, Percentage);
            }
            else if (Percentage > 80)
            {
                context.State = new GreenState(context, Percentage);
            }
        }
    }
}
