using System;

namespace EntityFramework
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
        public decimal Salary { get; set; }
        public byte [] Photo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IsworkingFor { get; set; }
    }
}
