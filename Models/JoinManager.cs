using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqJoinExample.Models 
{
    ///
    /// Summary:
    ///     This class impliments SQL's JOIN like: Inner, Left, Outer, LeftExcluding, Outer Excluding
    ///
    public class JoinManager 
    {
        ICollection<Person> _people = Person.Generate();
        ICollection<Equipment> _equipments = Equipment.Generate();

        ///
        /// Summary:
        ///     At this example we will make inner join between people and equipment
        ///     In LINQ we have Join method. 
        ///     1st argument: collection
        ///     2nd lambda fuction which return property in 1st collection
        ///     3td lambda function which return property in 2nd collection
        ///     last argument: labmda which return new anonim object
        ///
        public void InnerJoin()
        {
            // join person with orders
            var result = _people
                .Join(_equipments,
                    person => person.Id,
                    equip => equip.PersonId,
                    (person, equipment) => new { person, equipment })
                .GroupBy(x => x.person);

            foreach(var person in result)
            {
                Console.WriteLine(string.Format("{0}) {1}", person.Key.Id, person.Key.FullName));
                foreach(var equipment in person)
                {
                    Console.WriteLine(string.Format("{0} | {1}", equipment.equipment.Id, equipment.equipment.Name));
                }
            }
        }

        ///
        /// Summary:
        ///     Left join look like a inner join, but use another method: GroupJoin 
        ///     1st argument: collection
        ///     2nd lambda fuction which return property in 1st collection
        ///     3td lambda function which return property in 2nd collection
        ///     last argument: labmda which return new anonim object
        ///
        public void LeftJoin()
        {
            var result = _people
                .GroupJoin(_equipments,
                    prs => prs.Id,
                    equipment => equipment.PersonId,
                    (person, equipments) => 
                    new { person, equipments });
            
            foreach(var person in result)
            {
                Console.WriteLine(string.Format("{0}) {1}", person.person.Id, person.person.FullName));
                foreach(var equipment in person.equipments)
                {
                    Console.WriteLine(string.Format("{0} | {1}", equipment.Id, equipment.Name));
                }
            }

        }

        ///
        /// Summary:
        ///     In this example we use inner join, but after we use Exept method 
        ///     1st argument: collection
        ///     2nd lambda fuction which return property in 1st collection
        ///     3td lambda function which return property in 2nd collection
        ///     last argument: labmda which return new anonim object
        ///
        public void LeftExcludingJoin()
        {
            var innerJoin = _people
                .Join(_equipments,
                    person => person.Id,
                    equip => equip.PersonId,
                    (person, equipment) => person);
            
            var result = _people.Except(innerJoin);

            foreach(var item in result)
            {
                Console.WriteLine(string.Format("{0}) {1}", item.Id, item.FullName));
            }

        }

        ///
        /// Summary:
        ///     Unfortunetly, LINQ doesn't have methods for outer join, so we need use own algoritm 
        ///     We need make left join for people
        ///     We need make left join for equipments
        ///     Then we need use Union method
        ///
        public void OuterJoin()
        {

            var leftJoinPeople = _people
                .GroupJoin(_equipments,
                    prs => prs.Id,
                    equipment => equipment.PersonId,
                    (person, equipments) => new { Person = person, Equipments = equipments })
                .Select(x => 
                {
                    List<(Person person, Equipment equipment)> res = null;

                    res = x.Equipments.Any() ? 
                        x.Equipments.Select(f => (x.Person, f)).ToList() :
                        new List<(Person person, Equipment equipment)>() { (x.Person, null) }; 

                    return res;
                })
                .SelectMany(x => x)
                .Select(x => new { Person = x.person, Equipment = x.equipment });

            var leftJoinEquipment = _equipments
                .GroupJoin(_people,
                    equip => equip.PersonId,
                    person => person.Id,
                    (equip, prs) => new { Persons = prs, Equipment = equip })
                .Select(x => 
                {
                    List<(Person person, Equipment equipment)> res = null;

                    res = x.Persons.Any() ?
                        x.Persons.Select(f => (f, x.Equipment)).ToList() :
                        new List<(Person person, Equipment equipment)>() { (null, x.Equipment) }.ToList();
                    return res;
                })
                .SelectMany(x => x)
                .Select(x => new { Person = x.person, Equipment = x.equipment });

            var result = leftJoinPeople
                .Union(leftJoinEquipment)
                .ToList();

            foreach(var item in result)
            {
                Console.WriteLine(string.Format("{0}) {1} - {2}) {3}",
                    item.Person != null ? item.Person.Id.ToString() : null,
                    item.Person != null ? item.Person.FullName : null,
                    item.Equipment != null ? item.Equipment.Id.ToString() : null,
                    item.Equipment != null ? item.Equipment.Name : null ));
            }

        }

        ///
        /// Summary:
        ///     This sample works like outer join, but at last we added inner join and  
        ///     Except method
        ///
        public void OuterExcludingJoin()
        {
            var leftJoinPeople = _people
                .GroupJoin(_equipments,
                    prs => prs.Id,
                    equipment => equipment.PersonId,
                    (person, equipments) => new { Person = person, Equipments = equipments })
                .Select(x => 
                {
                    List<(Person person, Equipment equipment)> res = null;

                    res = x.Equipments.Any() ? 
                        x.Equipments.Select(f => (x.Person, f)).ToList() :
                        new List<(Person person, Equipment equipment)>() { (x.Person, null) }; 

                    return res;
                })
                .SelectMany(x => x)
                .Select(x => new { Person = x.person, Equipment = x.equipment });

            var leftJoinEquipment = _equipments
                .GroupJoin(_people,
                    equip => equip.PersonId,
                    person => person.Id,
                    (equip, prs) => new { Persons = prs, Equipment = equip })
                .Select(x => 
                {
                    List<(Person person, Equipment equipment)> res = null;

                    res = x.Persons.Any() ?
                        x.Persons.Select(f => (f, x.Equipment)).ToList() :
                        new List<(Person person, Equipment equipment)>() { (null, x.Equipment) }.ToList();
                    return res;
                })
                .SelectMany(x => x)
                .Select(x => new { Person = x.person, Equipment = x.equipment });

            var innerJoin = _people
                .Join(_equipments,
                    person => person.Id,
                    equip => equip.PersonId,
                    (person, equipment) => new { Person = person, Equipment = equipment });

            var result = leftJoinPeople
                .Union(leftJoinEquipment)
                .Except(innerJoin)
                .ToList();

            foreach(var item in result)
            {
                Console.WriteLine(string.Format("{0}) {1} - {2}) {3}",
                    item.Person != null ? item.Person.Id.ToString() : null,
                    item.Person != null ? item.Person.FullName : null,
                    item.Equipment != null ? item.Equipment.Id.ToString() : null,
                    item.Equipment != null ? item.Equipment.Name : null ));
            }
        }

    }
}