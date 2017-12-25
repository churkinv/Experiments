using System;
using System.Linq;
using System.Windows;

namespace EntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeDataContext context;

        public MainWindow()
        {
            InitializeComponent();
            context = new EmployeeDataContext();
            this.DataContext = context;
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {         

            Employee Mazepa = new Employee
            {
                Name = "Anonym",
                Surname = "MigrationFailed",
                IsworkingFor = "Ukraine"
            };
            
             //добавление
            context.Employees.Add(Mazepa);
            context.SaveChanges();
            //сохранение изменений
            MessageBox.Show("Successfully saved");                    

        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Employee employee;
            employee = context.Employees.FirstOrDefault();

            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Employee employee;

            using (EmployeeDataContext newcontext = new EmployeeDataContext())
            {
                // получаем первый объект
                employee = context.Employees.FirstOrDefault();

                employee.Salary = 35000;
                context.Entry(employee).State = System.Data.Entity.EntityState.Modified; // на тот случай если объект получен в одном контексте
                //а изменения в другом. Чтобы изменения сохранились, нам явным образом надо установить для его состояния значение EntityState.Modified
                employee.DateOfBirth = DateTime.Parse("2/2/2012 5:57:00 pm");
                context.SaveChanges();   // сохраняем изменения   
            }
        }

        /// <summary>
        /// сли объект получен в одном контексте, а сохраняется в другом, 
        /// то мы можем устанавливать у него вручную состояния 
        /// EntityState.Updated или EntityState.Deleted. 
        /// Но есть еще один способ: с помощью метода Attach у объекта DbSet 
        /// мы можем прикрепить объект к текущему контексту данных:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Attach_Click(object sender, RoutedEventArgs e)
        {
           Employee emp;
            using (EmployeeDataContext ctx = new EmployeeDataContext())
            {
                emp = ctx.Employees.FirstOrDefault();
            }
            // редактирование
            using (EmployeeDataContext ctx = new EmployeeDataContext())
            {
                if (emp != null)
                {
                    ctx.Employees.Attach(emp);
                    emp.Salary = 999;
                    ctx.SaveChanges();
                }
            }
            // удаление
            using (EmployeeDataContext ctx = new EmployeeDataContext())
            {
                if (emp != null)
                {
                    ctx.Employees.Attach(emp);
                    ctx.Employees.Remove(emp);
                    ctx.SaveChanges();
                }
            }
        }

        private void btn_UpdateMany_Click(object sender, RoutedEventArgs e)
        {          
            
        }
    }
}
