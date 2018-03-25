using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqJoinExample.Models 
{
    public class Equipment 
    {
        public static ICollection<Equipment> Generate()
        {
            var result = new List<Equipment>();

            result.Add(new Equipment(){ Id = 1, PersonId = 1, Name = "PC1" });
            result.Add(new Equipment(){ Id = 2, PersonId = 1, Name = "Book1" });
            result.Add(new Equipment(){ Id = 3, PersonId = 1, Name = "Notebook1" });
            result.Add(new Equipment(){ Id = 4, PersonId = 1, Name = "Wizard1" });
            result.Add(new Equipment(){ Id = 5, PersonId = 1, Name = "Pen1" });

            result.Add(new Equipment(){ Id = 6, PersonId = 2, Name = "PC2" });
            result.Add(new Equipment(){ Id = 7, PersonId = 2, Name = "Book2" });

            result.Add(new Equipment(){ Id = 8, PersonId = 3, Name = "PC3" });
            result.Add(new Equipment(){ Id = 9, PersonId = 3, Name = "Book3" });
            result.Add(new Equipment(){ Id = 10, PersonId = 3, Name = "Notebook3" });

            result.Add(new Equipment(){ Id = 11, PersonId = 4, Name = "Book4" });
            result.Add(new Equipment(){ Id = 12, PersonId = 4, Name = "Wizard4" });
            result.Add(new Equipment(){ Id = 13, PersonId = 4, Name = "Pen4" });

            result.Add(new Equipment(){ Id = 14, PersonId = 5, Name = "Notebook5" });

            result.Add(new Equipment(){ Id = 15, Name = "Ball" });

            return result;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int PersonId { get; set; }
    }
}