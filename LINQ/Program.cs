using RacconsLibraryCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person person = new Person();

           var person = Person.GetPersons();
            //{
            //  new Person() {Id=1, Name = "Petr", Mail = "Petr@gm.com" },
            //  new Person() {Id=2, Name = "Anna", Mail = "Anna@gm.com" },
            //  new Person() {Id=3, Name = "Ivan", Mail = "Ivan@gm.com" },
            //  new Person() {Id=4, Name = "Tanya", Mail = "Tanya@gm.com" },
            //  new Person() {Id=5, Name = "Vuyko", Mail = "Vuyko@gm.com" },
            //  new Person() {Id=6, Name = "Marichka", Mail = "Marichka@gm.com" },
            //  new Person() {Id=8, Name = "Sema", Mail = "Sema@gm.com" },
            //  new Person() {Id=8, Name = "Zinaida", Mail = "Zinaida@gm.com" }
            //};

            // 1
            //var query = from c in person.Persons
            //            where c.Id == 5
            //            select c.Mail;

            //Console.WriteLine(query.First());
            //Console.ReadLine();

            // 2
            //var result = person.Persons.First(c=>
            //c.Id == 10); // but will be exception if no element exist better to use

            //var result2 = person.Persons.FirstOrDefault(c =>
            //c.Id == 10); 

            //Console.WriteLine(result.Name);
            //Console.ReadLine();

            //3
            //var result = person.Persons.FirstOrDefault(c =>
            //{
            //    Debug.WriteLine(c.Name);
            //    return c.Id == 8;
            //});

            //var result = person.Persons.Where(c =>
            //c.Id == 8)
            //.Skip(1)
            //.FirstOrDefault();


            //Console.WriteLine(result.Name);
            //Console.ReadLine();

            //foreach (var person in SortByType(Person.GetPersons()))
            //{
            //    Console.WriteLine(person.Name +" "+person.Mail);
            //}

            ICollection<Person> list = Person.GetPersons();

            var query = from c in list
                        where c.Id == 1                        
                        select c.Name;
            //foreach (var item in query)
            //{
                Console.WriteLine(query.FirstOrDefault());
            //}
            var query2 = list.OrderBy(c => c.Name)
                .ThenByDescending(c => c.Name)
                .ToList();

            foreach(var item in query2)
                Console.WriteLine(item.Name);

            Console.ReadLine();
        }

        public static IEnumerable<Person> SortByName(List<Person> persons)
        {
            return persons.OrderBy(p => p.Name)
                .ThenBy(p => p.Mail);
        }

        public static IEnumerable<Person> SortByNameReverse(List<Person> persons)
        {
            return persons.OrderByDescending(p => p.Name)
                .ThenByDescending(p => p.Mail);

            // or:
            // return SortByName(persons).Reverse();
        }

        public static IEnumerable<Person> SortByType(List<Person> persons)
        {
            return persons.OrderByDescending(p => p.Id.HasValue); // true will come first null last        
        }

        public IEnumerable<int> BuildSequence()
        {
            var integers = Enumerable.Range(0, 10)
                .Select(i => 5 + (10 * i));
            return integers;

            //example of repeat
            //var integers = Enumerable.Repeat(-1, 10);
            //return integers;
        }

        public IEnumerable<string> BuildSequenceString()
        {
            Random rnd = new Random();
            var strings = Enumerable.Range(0, 10)
                .Select(i => ((char)('A' + rnd.Next(0, 26))).ToString());
            return strings;
        }

        public static dynamic GetNamesAndType(List<Person> persons,
            List<string> parties)
        {
            var query = persons.Join(parties,
                c => c.Name,
                ct => ct.PersonType,
                (c, ct) => new
                {
                    Name = c.Name + ", " + c.Mail,
                    PersonType = ct.Type
                });
            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ": " + item.CustomerTypeName);
            }
            return query;
        }
    }
}
