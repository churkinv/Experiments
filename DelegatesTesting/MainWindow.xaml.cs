using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace DelegatesTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        //string Name;
        //Messages message = new Messages();
        //public static TextBox txt_Name; 
        public static TextBox t; // это для того чтобы из другого класса можно было передать значение в текстбокс
        Messages myClass = new Messages();
        


        public MainWindow()
        {
            InitializeComponent();
            //t = txt_Name; // так присваиваем значение к реальному текстбоксу
            t = txt_Check;

            myClass.MessageHandler += Check;

        }
        
        public void Check(string hi)
        {
            Dispatcher.Invoke(() =>
            {
                txt_Check.Text += "\n"+hi;
            });
        }

        public void getName(InputMessageFromTextBox a)
        {
            //a(txt_Name.Text);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Task.Factory.StartNew(() =>
            { myClass.TCPserver(); });
           
          //myClass.MessageHandler += Check;
          //Messages.Message();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myClass.TCPclient();
        }
    }


}
