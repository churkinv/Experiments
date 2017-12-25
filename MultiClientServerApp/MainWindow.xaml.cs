using Network.P2P;
using RaccoonsLibraryCommon;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MultiClientServerApp
{

    public partial class MainWindow : Window
    {
        static CompanyNetwork company = new CompanyNetwork("Company", "test");
        Server server = new Server(IPAddress.Any, 12000); // new Server(company);
        Client client = null;// new Client("127.0.0.1", 12000);
        CancellationTokenSource cts;
        Task task = null;

        public MainWindow()
        {
            DataTransfer.MessageToPass += ShowMeInfo;
            Server.MessageToPass += ShowMeInfo;
            Client.MessageToPass += ShowMeInfo;
            Peer.OnMessageHandler += ShowMeInfo;

            InitializeComponent();
            btn_StopServer.IsEnabled = false;
        }

        public void ShowMeInfo(string message)
        {
            MessageBox.Show(message);
            Dispatcher.Invoke(() =>
            {
                lbl_TextFromServer.Content = message;
            });
        }

        private void btn_StartServer_Click(object sender, RoutedEventArgs e)
        {
            server = new Server(IPAddress.Any, 12000);
            try
            {
                Dispatcher.Invoke(() =>
                {
                    btn_StopServer.IsEnabled = true;
                    btn_StartServer.IsEnabled = false;
                    //btn_Send.IsEnabled = false;
                    txt_IPaddress.IsEnabled = false;
                });

                /* /* task =  Task.Run(async()  => { await*/
                server.ListenAsync();/* }); */
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка :\n {ex}");
            }
        }

        private void btn_StopServer_Click(object sender, RoutedEventArgs e)
        {
            server.Disconnect();
            //task.ConfigureAwait(true)
            //    .GetAwaiter()
            //    .OnCompleted(() =>
            //    {        
            btn_StartServer.IsEnabled = true;
            btn_StopServer.IsEnabled = false;
            txt_IPaddress.IsEnabled = true;
            btn_Send.IsEnabled = true;
            //}); //to come back to the task and do some work after smth accomplished
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            string name = "D`Artanyan";
            Peer peer = new Peer(name);
            string ip;
            Int32 port = 12000;
            TcpClient client;
            NetworkStream stream;
            IFormatter formatter = new BinaryFormatter();

            if (String.IsNullOrEmpty(txt_IPaddress.Text))
            {
                ip = "127.0.0.1";
            }
            else
            {
                ip = txt_IPaddress.Text;
            }


            client = new TcpClient();
            try
            {
                client.Connect(IPAddress.Parse(ip), port);
                stream = client.GetStream();
                Data hello = new Data(name);
                byte[] bytes = name.GetBytes();
                DataTransfer.SendData(bytes, stream);
            }
            catch (Exception ex)
            {
                ShowMeInfo("Щось трапилось");
            }
        }

    }
}

