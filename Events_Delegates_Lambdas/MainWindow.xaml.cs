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

namespace Events_Delegates_Lambdas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txt_Test.Text = "hi";
        }

        public void ChangeText()
        {

            txt_Test.Text = "? from Method\n";
            //Thread.Sleep(3000);
            txt_Test.Text += (2 + 4).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeText();
        }
    }
}
