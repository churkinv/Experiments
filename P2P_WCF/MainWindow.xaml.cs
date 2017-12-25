using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceModel;
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
using System.Net.PeerToPeer;
using System.Threading;
using System.Windows.Threading;
using System.Net.Sockets;

namespace P2P_WCF
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {

        #region fields for Chat Server (Server and Client object classes)

        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания

        #endregion

        #region field for Client of chat (Server and Client object classes)
        static string userName;
        //private const string host = "127.0.0.1"; declared in the WPF
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        #endregion

        #region To call Console in WPF
        /// <summary>
        /// first variant to call a console, to call it in code use  AllocConsole(); but i can`t output text from WPF
        /// I could do so earlier, but can`t now, as sollution tried to use ConsoleManager Class 
        /// from SO, the same issue,
        /// third variant worked: "Right click on the project, "Properties", "Application" tab, 
        /// change "Output Type" to "Console Application", and then it will also have a console."
        /// </summary>

        [DllImport("Kernel32")] // 
        public static extern void AllocConsole();
        [DllImport("Kernel32")]
        public static extern void FreeConsole();
        #endregion
       
        public MainWindow()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InitializeComponent();
            string _networkName = "No";
            int _pcNumber = 0;

            lbl_YourIp.Content = $"Your IP: {NetworkCooperation.GetMyLocalIPv4().ToString()}";
            lbl_Info.Content = $"You are part of the \"{_networkName}\" network";
            lbl_PCaround.Content = $"You have {_pcNumber}  PC active around";
            lbl_ThisPCHostName.Content = $"My Host Name \"{Dns.GetHostName()}\"";
            Console.WriteLine(System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName);
            //Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());

        }

        private void Work ()
        {

            for (int i = 0; i <= 50; i++)
            {
                Updater uiUpdater = new Updater(UpdateUI);
                Dispatcher.BeginInvoke(DispatcherPriority.Send, uiUpdater, i);
                //Thread.Sleep(500);
            }
            
        }

        private delegate void Updater(string UI);

        private void UpdateUI(string i)
        {
            i = "You are TCP server righ now";
          lbl_Info.Content = i;
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private async void btn_DistrFile_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txt_IPAddress.Text);
            string path = txt_Send.Text;
            await Task.Factory.StartNew(() => NetworkCooperation.SendFile(path, 25000, ip),
                                               TaskCreationOptions.LongRunning);
            
        }

        private void btn_RegisterPeer_Click(object sender, RoutedEventArgs e)
        {
            PeerName peerName = new PeerName(txt_PeerName.Text, PeerNameType.Unsecured);
            using (PeerNameRegistration registration = new PeerNameRegistration(peerName, Int32.Parse(txt_Port.Text))) 
            {
                registration.Comment = "My Comment";
                string time = string.Format("Peer Created at: {0}", DateTime.Now.ToString());
                UnicodeEncoding encoder = new UnicodeEncoding();
                byte[] data = encoder.GetBytes(time);

                try
                {
                    foreach (Cloud c in Cloud.GetAvailableClouds())
                        Console.WriteLine($"Cloud {c.Name}");
                    registration.Start();
                    Console.WriteLine("Registered");
                    Console.ReadLine();
                    //registration.Stop();
                }
                catch (PeerToPeerException ex)
                {
                    // There are other possible statndard exceptions to catch 
                    // See documentation on for details
                }
            }
        
        }

        private async void btn_TCPserver_Click(object sender, RoutedEventArgs e)
        {
            btn_TCPclient.IsEnabled = false;
            btn_TCPserver.IsEnabled = false;
            lbl_Info.Content = "You are server now";

            if (String.IsNullOrWhiteSpace(txt_Port.Text))
            {
                if (String.IsNullOrEmpty(txt_Port.Text))
                {
                    txt_GeneralChat.Text = "You didn`t specify the port. Used default port for app: 25000";
                    //await Task.Factory.StartNew(() => NetworkCooperation.TcpConnectionServer(25000/*, NetworkCooperation.GetMyLocalIPv4()*/),
                    //                             TaskCreationOptions.LongRunning);
                    //await Task.Factory.StartNew(() => NetworkCooperation.SocketServerTCP(25000, NetworkCooperation.GetMyLocalIPv4().ToString()),
                    //                             TaskCreationOptions.LongRunning);


                }
            }

            else
            {
                #region Some Thread explanation
                //This code:
                // Thread t_PerthOut = new Thread(new ThreadStart(ReadCentralOutQueue("test"));
                //tries to call ReadCentralOutQueue and then create a delegate from the result.That isn't going to work, because it's a void method. Normally you'd use a method group to create a delegate, or an anonymous function such as a lambda expression. In this case a lambda expression will be easiest:
                //Thread t_PerthOut = new Thread(() => ReadCentralOutQueue("test"));
                //You can't just use new Thread(ReadCentralOutQueue) as the ReadCentralOutQueue doesn't match the signature for either ThreadStart or ParameterizedThreadStart.
                //******************************
                //In WPF, only the thread that created a DispatcherObject may access that object.For example, 
                //a background thread that is spun off from the main UI thread cannot update the contents of 
                //a Button that was created on the UI thread.In order for the background thread to access 
                //the Content property of the Button, the background thread must delegate the work to the 
                //Dispatcher associated with the UI thread.This is accomplished by using either Invoke or 
                //BeginInvoke. Invoke is synchronous and BeginInvoke is asynchronous.The operation is added
                //to the event queue of the Dispatcher at the specified DispatcherPriority.
                //******************************
                // that is why this won`t work in WPF
                //Thread myThread = new Thread(()=>NetworkCooperation.TcpConnectionServer(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4()));
                //myThread.Start(); // запускаем поток
                //******************************
                //Since.NET 4.5 and C# 5.0 you should use Task-based Asynchronous Pattern (TAP) along with async-await keywords in all areas (including the GUI):
                //TAP is the recommended asynchronous design pattern for new development


                #endregion

                #region эксперименты
                //Thread myThread = new Thread(()=>NetworkCooperation.TcpConnectionServer(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4()));
                //myThread.Start(); // запускаем поток

                //this.Dispatcher.Invoke((Action)(() =>
                //{
                //    NetworkCooperation.TcpConnectionServer(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4());
                //}));


                //Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(() =>
                //{
                //    NetworkCooperation.TcpConnectionServer(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4());
                //}));

                //await Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(() =>
                //{
                //    NetworkCooperation.TcpConnectionServer(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4());

                //}));

                #endregion

                var port = Int32.Parse(txt_Port.Text);
                ////var progress = new Progress<string>(s => label.Text = s);
                //await Task.Factory.StartNew(() => NetworkCooperation.TcpConnectionServer(port/*, NetworkCooperation.GetMyLocalIPv4()*/), 
                //                                  TaskCreationOptions.LongRunning);

                await Task.Factory.StartNew(() => NetworkCooperation.SocketServerTCP(port, NetworkCooperation.GetMyLocalIPv4().ToString()),
                TaskCreationOptions.LongRunning);
                //Console.WriteLine();

                //await Task.Run(()=> Dispatcher.Invoke(() =>
                //{
                //    NetworkCooperation.SocketServerTCP(Int32.Parse(txt_Port.Text), NetworkCooperation.GetMyLocalIPv4().ToString());
                //}));

                //var task = Task.Run(() =>
                //{

                //}); // решил проблему с вводом данных сразу в функцию но потерял ассинхронность, форма виснет
            }

        }

        private async void btn_TCPclient_Click(object sender, RoutedEventArgs e)
        {
            btn_TCPserver.IsEnabled = false;

            if (String.IsNullOrWhiteSpace(txt_Port.Text) || String.IsNullOrWhiteSpace(txt_IPAddress.Text))
            {
                if (String.IsNullOrEmpty(txt_Port.Text) || String.IsNullOrEmpty(txt_IPAddress.Text))
                {
                    txt_GeneralChat.Text = "You didn`t specify IP and Port.Default settings are used:\n" +
                       "port: 25000\n" +
                       "IP: 192.168.0.100-103";
                    var message = txt_Send.Text;
                    //var progress = new Progress<string>(s => label.Text = s);
                    await Task.Factory.StartNew(() => NetworkCooperation.TcpConnectionClient("192.168.0.102", 25000, message),
                                                      TaskCreationOptions.LongRunning);
                    //var ip = txt_IPAddress.Text;
                    //await Task.Factory.StartNew(() => NetworkCooperation.SocketClientTCP(25000, ip),
                    //                                 TaskCreationOptions.LongRunning);
                }
            }

            else
            {
                var _port = Int32.Parse(txt_Port.Text);
                var _ip = txt_IPAddress.Text;
                var message = txt_Send.Text;
                //var progress = new Progress<string>(s => label.Text = s);
                await Task.Factory.StartNew(() => NetworkCooperation.TcpConnectionClient(_ip, _port, message),
                                                  TaskCreationOptions.LongRunning);

                //await Task.Factory.StartNew(() => NetworkCooperation.SocketClientTCP(_port,_ip),
                //                                 TaskCreationOptions.LongRunning);

            }
        }

        public void MessageToTextbox(string message) // это для делегата по отправке сообщение в текстбокс
        {
            Dispatcher.Invoke(() =>
            {
                txt_GeneralChat.Text += message + "\n";
            });
        }

        public void MessageInput(string message)
        {
            Dispatcher.Invoke(() =>
            {
                txt_GeneralChat.Text += message + "/n";
            });
        }
           
       
        private async void btn_ChatServer_Click(object sender, RoutedEventArgs e)
        {
            btn_ChatServer.IsEnabled = false;
            SendMessageToTextBox send = MessageToTextbox;
            try
            {
                
                server = new ServerObject();
                //server.Listen(send);
                //listenThread = new Thread(new ThreadStart(server.Listen(send)));
                //listenThread.Start(); //старт потока

                await Task.Factory.StartNew(() => server.Listen(send));

            }
            catch (Exception ex)
            {
                server.Disconnect();
                Dispatcher.Invoke(() =>
                {
                    txt_GeneralChat.Text += ex.Message + "\n";
                });
                
            }
        }

        private void btn_ChatClient_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                txt_GeneralChat.Text += "Введите свое имя \n";
            });
          

            userName = Console.ReadLine();
            client = new TcpClient();
            try
            {
                string host = txt_IPAddress.Text;
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                Console.WriteLine("Добро пожаловать, {0}", userName);
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        // отправка сообщений
        static void SendMessage()
        {
            Console.WriteLine("Введите сообщение: ");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        // получение сообщений
        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    Dispatcher.Invoke(() =>
                    {
                        txt_GeneralChat.Text += "\n" + message;//вывод сообщения
                    });
                    
                }
                catch
                {
                    Dispatcher.Invoke(() =>
                    {
                        txt_GeneralChat.Text += "\n" + "Подключение прервано!"; //соединение было прервано
                    });

                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }


    }
}
