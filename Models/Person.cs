using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqJoinExample.Models 
{
    public class Person 
    {
        public static ICollection<Person> Generate()
        {
            var result = new List<Person>();

            result.Add(new Person(){ Id = 1, FullName = "Tomas" });
            result.Add(new Person(){ Id = 2, FullName = "Sam" });
            result.Add(new Person(){ Id = 3, FullName = "Smith" });
            result.Add(new Person(){ Id = 4, FullName = "Den" });
            result.Add(new Person(){ Id = 5, FullName = "Clark" });
            result.Add(new Person(){ Id = 6, FullName = "Luke" });

            return result;
        }

        public int Id { get; set; }

        public string FullName { get; set; }
    }
}