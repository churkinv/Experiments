using RacconsLibraryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person person;

        public MainWindow()
        {
            InitializeComponent();
            // 1) DataContext = Person.GetPersons(); // one of ways to create source for data binding in xaml

            // 2) 
            //person = new Person()
            //{
            //    Name = "Valera",
            //    Mail = "valera@m.com"

            // };
            //DataContext = person;

            // 3)
            //DataContext = Person.GetPersonsObservable();

            //4) DataGrid
            //Data.ItemsSource = Person.GetPersonsObservable();

        }

        private void btn_Change_Click(object sender, RoutedEventArgs e)
        {
            person.Name = "Thor";
            person.Mail = "new@mail.com";
        }

        private void btn_help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yo yo");
        }
        //private void btn_Save_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Full Name: ");
        //    sb.Append(txt_Name.Text);
        //    sb.Append(" Sex: ");
        //    sb.Append((bool)rdb_Male.IsChecked ? "Male" : "Female");
        //    sb.Append("Computer: ");
        //    sb.Append((bool)chkbx_Desctop.IsChecked ? "Desktop" : "");
        //    sb.Append((bool)chkbx_Laptop.IsChecked ? "Laptop" : "");
        //    sb.Append((bool)chkbx_Tablet.IsChecked ? "Tablet" : "");
        //    sb.Append("Your job: ");
        //    sb.Append(cmb_Job.SelectedItem.ToString());
        //    sb.Append(clndr_Calendar.SelectedDate.ToString());
        //    MessageBox.Show(sb.ToString());
        //}


    }
}
