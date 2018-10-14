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

namespace MultiThreadingWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TextBlock 
        }
        async static Task<string> GetStringASync()
        {
            var result = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Yo ho ho";
            });

            return result;
        }

        async static void Print()
        {
            Console.WriteLine(await GetStringASync());
        }

        private async void btn_button_Click(object sender, RoutedEventArgs e)
        {
            btn_button.Content = await GetStringASync();
        }

        private void btn_button2_Click(object sender, RoutedEventArgs e)
        {
            var result = Task.Run( () =>
            {
                Thread.Sleep(4000);
                return  "Second test";               
            });
            result.ConfigureAwait(true) // this constructions actully is ancestor of async/await(?) We are configured awaiter to go back to UI thread
                  .GetAwaiter()        // so no need in using Dispatecher
                  .OnCompleted(() => btn_button2.Content = result.Result);

        }
    }
    class ThreadTest
    {
    }

}

