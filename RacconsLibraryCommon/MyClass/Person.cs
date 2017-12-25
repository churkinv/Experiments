using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon
{
    /// <summary>
    /// For testing
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public List <Person> Persons { get; set; }

        public Person()
        {
            Persons = new List<Person>()
            {
              new Person() {Id=1, Name = "Petr", Mail = "Petr@gm.com" },
              new Person() {Id=2, Name = "Anna", Mail = "Anna@gm.com" },
              new Person() {Id=3, Name = "Ivan", Mail = "Ivan@gm.com" },
              new Person() {Id=4, Name = "Tanya", Mail = "Tanya@gm.com" },
              new Person() {Id=5, Name = "Vuyko", Mail = "Vuyko@gm.com" },
              new Person() {Id=6, Name = "Marichka", Mail = "Marichka@gm.com" },
              new Person() {Id=7, Name = "Sema", Mail = "Sema@gm.com" },
              new Person() {Id=8, Name = "Zinaida", Mail = "Zinaida@gm.com" }
            };
        }

        public void AddPerson(Person newPerson)
        {
            Persons.Add(newPerson);
        }

        public void RemovePerson(int id)
        {
            if (id < Persons.Count()) Persons.RemoveAt(id);
            else MessageBox.Show("Out of List range");
        }

    }
}
