using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P08b_Interpeter
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            Context c = new Context(s);
            List<Expression> exprs = new List<Expression>();
            exprs.Add(new ThousandExpression());
            exprs.Add(new HundredExpression());
            exprs.Add(new TenExpression());
            exprs.Add(new OneExpression());

            foreach (Expression exp in exprs)
            {
                exp.Interpret(c);
            }
            Console.WriteLine(s + " = " + c.Output);
            Console.ReadLine();

        }
    }

    public class Context
    {
        public string Input { get; set; }
        public int Output { get; set; }

        public Context(string input)
        {
            Input = input;
        }
    }


    abstract class Expression
    {
        public void Interpret(Context ctx)
        {
            if (ctx.Input.Length == 0)
            {
                return;
            }

            if (ctx.Input.StartsWith(Nine()))
            {
                ctx.Output += (9 * Multiplier());
                ctx.Input = ctx.Input.Substring(2);
            }
            else if (ctx.Input.StartsWith(Four()))
            {
                ctx.Output += (4 * Multiplier());
                ctx.Input = ctx.Input.Substring(2);
            }
            else if (ctx.Input.StartsWith(Five()))
            {
                ctx.Output += (5 * Multiplier());
                ctx.Input = ctx.Input.Substring(1);
            }
            while (ctx.Input.StartsWith(One()))
            {
                ctx.Output += (Multiplier());
                ctx.Input = ctx.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }

    class ThousandExpression : Expression
    {

        public override string One()
        {
            return "M";
        }

        public override string Four()
        {
            return " ";
        }

        public override string Five()
        {
            return " ";
        }

        public override string Nine()
        {
            return " ";
        }

        public override int Multiplier()
        {
            return 1000;
        }
    }

    class HundredExpression : Expression
    {

        public override string One()
        {
            return "C";
        }

        public override string Four()
        {
            return "CD";
        }

        public override string Five()
        {
            return "D";
        }

        public override string Nine()
        {
            return "CM";
        }

        public override int Multiplier()
        {
            return 100;
        }
    }

    class TenExpression : Expression
    {

        public override string One()
        {
            return "X";
        }

        public override string Four()
        {
            return "XL";
        }

        public override string Five()
        {
            return "L";
        }

        public override string Nine()
        {
            return "XC";
        }

        public override int Multiplier()
        {
            return 10;
        }
    }

    class OneExpression : Expression
    {

        public override string One()
        {
            return "I";
        }

        public override string Four()
        {
            return "IV";
        }

        public override string Five()
        {
            return "V";
        }

        public override string Nine()
        {
            return "IX";
        }

        public override int Multiplier()
        {
            return 1;
        }
    }

}