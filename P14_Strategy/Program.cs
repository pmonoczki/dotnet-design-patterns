using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P14_Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            People chars = new People();
            chars.Add(new Character { Name = "Peter Griffin", Age = 35, City = "Quahog" });
            chars.Add(new Character { Name = "Eric Cartman", Age = 8, City = "South Park" });
            chars.Add(new Character { Name = "Homer Simpson", Age = 45, City = "Springfield" });
            chars.Add(new Character { Name = "Ted Mosby", Age = 29, City = "New York" });
            chars.Add(new Character { Name = "Dexter Morgan", Age = 35, City = "Miami" });
            chars.Add(new Character { Name = "Barney Stinson", Age = 32, City = "New York" });
            
            chars.Strategy = new NameSort();
            chars.Sort();

            chars.Strategy = new AgeSort();
            chars.Sort();

            chars.Strategy = new CitySort();
            chars.Sort();

            chars.Strategy = new CityAndName();
            chars.Sort();

            Console.ReadLine();
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} éves és {2}-ban lakik.",
                Name, Age, City);
        }
    }

    abstract class SortStrategy
    {
        public abstract void Sort(List<Character> list);
    }

    class NameSort : SortStrategy
    {
        public override void Sort(List<Character> list)
        {
            list.Sort((a, b) => a.Name.CompareTo(b.Name));
            Console.WriteLine("Név alapján rendezve.");
        }
    }

    class AgeSort : SortStrategy
    {
        public override void Sort(List<Character> list)
        {
            list.Sort((a, b) => a.Age.CompareTo(b.Age));
            Console.WriteLine("Kor alapján rendezve.");
        }
    }

    class CitySort : SortStrategy
    {
        public override void Sort(List<Character> list)
        {
            list.Sort((a, b) => a.City.CompareTo(b.City));
            Console.WriteLine("Lakhely alapján rendezve.");
        }
    }


    class CityAndName : SortStrategy
    {

        public override void Sort(List<Character> list)
        {
            var list2 = list.OrderBy(c => c.City).ThenBy(c => c.Name).ToList();
            list.Clear();
            list.AddRange(list2);
        }
    }

    class People
    {
        List<Character> list = new List<Character>();
        public SortStrategy Strategy { get; set; }

        public void Add(Character c)
        {
            list.Add(c);
        }

        public void Sort()
        {
            Strategy.Sort(list);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }

}
