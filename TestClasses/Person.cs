using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon
{
    public enum Party
    {
        Independent,
        Federalist,
        DemocratRepublican
    }


    /// <summary>
    /// For testing
    /// </summary>
    public class Person : INotifyPropertyChanged
    {
        public int? Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private Party _affiliation;

        public Party Affiliation
        {
            get { return _affiliation; }
            set
            {
                _affiliation = value;
                OnPropertyChanged();
            }
        }
        
        public static List<Person> Persons { get; set; }

        public Person()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "") // caller member passes the name of property which called this method, 
                                                                              // after that we can`t use auto properties as we have to have backing private field and call this method in setter
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public void AddPerson(Person newPerson)
        {
            Persons.Add(newPerson);
        }

        public void RemovePerson(int id)
        {
            if (id < Persons.Count()) Persons.RemoveAt(id);
            //else MessageBox.Show("Out of List range");
        }

        public static List<Person> GetPersons()
        {
            Persons = new List<Person>()
            {
              new Person() {Id=1, Name = "Petr", Mail = "Petr@gm.com" },
              new Person() {Id=2, Name = "Anna", Mail = "Anna@gm.com" },
              new Person() {Id=3, Name = "Ivan", Mail = "Ivan@gm.com" },
              new Person() {Id=null, Name = "Tanya", Mail = "Tanya@gm.com" },
              new Person() {Id=5, Name = "Vuyko", Mail = "Vuyko@gm.com" },
              new Person() {Id=6, Name = "Marichka", Mail = "Marichka@gm.com" },
              new Person() {Id=8, Name = "Sema", Mail = "Sema@gm.com" },
              new Person() {Id=8, Name = "Zinaida", Mail = "Zinaida@gm.com" }
            };

            return Persons;
        }

        public static ObservableCollection<Person> GetPersonsObservable()
        {
            var persons = new ObservableCollection<Person>();

            persons.Add(new Person() { Id = 1, Name = "PetrO", Mail = "Petr@gm.com" });
            persons.Add(new Person() { Id = 2, Name = "AnnaO", Mail = "Anna@gm.com" });
            persons.Add(new Person() { Id = 3, Name = "IvanO", Mail = "Ivan@gm.com" });
            persons.Add(new Person() { Id = 4, Name = "TanyaO", Mail = "Tanya@gm.com" });
            persons.Add(new Person() { Id = 5, Name = "VuykoO", Mail = "Vuyko@gm.com" });
            persons.Add(new Person() { Id = 6, Name = "MarichkaO", Mail = "Marichka@gm.com" });
            persons.Add(new Person() { Id = 8, Name = "SemaO", Mail = "Sema@gm.com" });
            persons.Add(new Person() { Id = 8, Name = "ZinaidaO", Mail = "Zinaida@gm.com" });

            return persons;
        }

    }
}
