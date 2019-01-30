using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P09_Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            OverIndexableList<int> oil = new OverIndexableList<int>();
            for (int i = 0; i < 10; i++)
            {
                oil[i] = i;
            }


            IEnumerator<int> enumerator = oil.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            //foreach (var item in oil)
            //{
            //    Console.WriteLine(item);
            //}
            Console.ReadLine();
        }
    }
}
