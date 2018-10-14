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
            string input = "asssdfdfghghg";
            Console.WriteLine(input.GroupBy(c => c).OrderByDescending(g => g.Count()).First().Key); // to show the most frequent char

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

        //public static dynamic GetNamesAndType(List<Person> persons,
        //    List<string> parties)
        //{
        //    var query = persons.Join(parties,
        //        c => c.Name,
        //        ct => ct.PersonType,
        //        (c, ct) => new
        //        {
        //            Name = c.Name + ", " + c.Mail,
        //            PersonType = ct.Type
        //        });
        //    foreach (var item in query)
        //    {
        //        Console.WriteLine(item.Name + ": " + item.CustomerTypeName);
        //    }
        //    return query;
        //}
    }


}
#region LINQ
//Select: определяет проекцию выбранных значений

//Where: определяет фильтр выборки

//OrderBy: упорядочивает элементы по возрастанию

//OrderByDescending: упорядочивает элементы по убыванию

//ThenBy: задает дополнительные критерии для упорядочивания элементов возрастанию

//ThenByDescending: задает дополнительные критерии для упорядочивания элементов по убыванию

//Join: соединяет две коллекции по определенному признаку

//GroupBy: группирует элементы по ключу

//ToLookup: группирует элементы по ключу, при этом все элементы добавляются в словарь

//GroupJoin: выполняет одновременно соединение коллекций и группировку элементов по ключу

//Reverse: располагает элементы в обратном порядке

//All: определяет, все ли элементы коллекции удовлятворяют определенному условию

//Any: определяет, удовлетворяет хотя бы один элемент коллекции определенному условию

//Contains: определяет, содержит ли коллекция определенный элемент

//Distinct: удаляет дублирующиеся элементы из коллекции

//Except: возвращает разность двух коллекцию, то есть те элементы, которые содератся только в одной коллекции

//Union: объединяет две однородные коллекции

//Intersect: возвращает пересечение двух коллекций, то есть те элементы, которые встречаются в обоих коллекциях

//Count: подсчитывает количество элементов коллекции, которые удовлетворяют определенному условию

//Sum: подсчитывает сумму числовых значений в коллекции

//Average: подсчитывает cреднее значение числовых значений в коллекции

//Min: находит минимальное значение

//Max: находит максимальное значение

//Take: выбирает определенное количество элементов

//Skip: пропускает определенное количество элементов

//TakeWhile: возвращает цепочку элементов последовательности, до тех пор, пока условие истинно

//SkipWhile: пропускает элементы в последовательности, пока они удовлетворяют заданному условию, и затем возвращает оставшиеся элементы

//Concat: объединяет две коллекции

//Zip: объединяет две коллекции в соответствии с определенным условием

//First: выбирает первый элемент коллекции

//FirstOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию

//Single: выбирает единственный элемент коллекции, если коллекция содердит больше или меньше одного элемента, то генерируется исключение

//SingleOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию

//ElementAt: выбирает элемент последовательности по определенному индексу

//ElementAtOrDefault: выбирает элемент коллекции по определенному индексу или возвращает значение по умолчанию, если индекс вне допустимого диапазона

//Last: выбирает последний элемент коллекции

//LastOrDefault: выбирает последний элемент коллекции или возвращает значение по умолчанию
#endregion