using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EntityFramework
{
    public class EmployeeDataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDataContext() : base("EntityFrameworkTest")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();           
        }
    }
}

