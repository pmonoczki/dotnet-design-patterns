using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace P08a_Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("WhatToDo.xml");

            var contexts = from c in doc.Descendants("NormalMethodCall")
                           select new Context { Call = c };

            List<MethodCallExpression> exprs = new List<MethodCallExpression>();
            exprs.Add(new GetCallInfoExpression());
            exprs.Add(new MakeCallExpression());

            foreach (var item in contexts)
            {
                foreach (var ex in exprs)
                {
                    ex.Interpret(item);
                    Console.WriteLine(item.Result);
                }
            }

            Console.Read();
        }
    }

    public class SomeClass
    {
        public string GetString()
        {
            return "hello";
        }
    }


    public abstract class MethodCallExpression
    {
        public abstract void Interpret(Context ctx);
    }

    public class Context
    {
        public XElement Call { get; set; }
        public object Result { get; set; }
    }

    public class GetCallInfoExpression : MethodCallExpression
    {

        public override void Interpret(Context ctx)
        {
            if (ctx.Call != null && ctx.Call.HasAttributes)
            {
                ctx.Result = string.Format(
                    "Class: {0}, Method: {1}",
                    (string)ctx.Call.Attribute("Class"),
                    (string)ctx.Call.Attribute("Method"));
            }
        }
    }

    public class MakeCallExpression : MethodCallExpression
    {

        public override void Interpret(Context ctx)
        {
            if (ctx.Call != null && ctx.Call.HasAttributes)
            {
                Type t = Type.GetType((string)ctx.Call.Attribute("Class"));
                object o = Activator.CreateInstance(t);
                MethodInfo mi = t.GetMethod((string)ctx.Call.Attribute("Method"));
                ctx.Result = mi.Invoke(o, new object[] { });
            }
        }
    }
}