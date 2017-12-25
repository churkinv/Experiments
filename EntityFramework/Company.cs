using System.Collections.Generic;

namespace EntityFramework
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
