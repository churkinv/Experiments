using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace NetworkDataTrasfer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region client 
        TcpClient client = new TcpClient(); // for server
        TcpClient clientS = new TcpClient(); // for client
        NetworkStream dataStream = null;

        byte[] dataToSend = new byte[256];
        byte[] dataToReceive = new byte[256];

        StringBuilder response = new StringBuilder();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            lbl_Yourip.Content = NetworkCooperation.GetMyLocalIPv4();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) //client
        {
            //TcpClient client = new TcpClient();

            try
            {
                string ip = txt_ServerIP.Text;
                int port = 10000;//Int32.Parse(txt_PortToConnect.Text);
                await Task.Factory.StartNew(() =>
                {
                    client.Connect(ip, port);
                    Dispatcher.Invoke(() =>
                    {
                        btn_ConnectToServer.IsEnabled = false;
                    });

                });

                Dispatcher.Invoke(() =>
                {
                    txt_Info.Text += "You are connected to server";
                });

                dataStream = client.GetStream();
                do
                {
                    int bytes = dataStream.Read(dataToReceive, 0, dataToReceive.Length);

                    Dispatcher.Invoke(() =>
                    {
                        txt_Info.Text += response.Append(Encoding.UTF8.GetString(dataToReceive, 0, bytes)).ToString();
                    });
                }

                while (dataStream.DataAvailable);
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    txt_Info.Text = $"You probably have some connection problem:\n{ex.Message}";
                });
            }
           
            finally
            {
                dataStream.Close();
                client.Close();
            }
        }

        private async void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            //NetworkStream dataStreamSend = client.GetStream();
            //NetworkStream dataStreamReceive = clientS.GetStream();
            TcpClient client2 = new TcpClient();
            string ip = txt_ServerIP.Text;
            int port = 10000;//Int32.Parse(txt_PortToConnect.Text);
            await Task.Factory.StartNew(() =>
            {
                client2.Connect(ip, port);
            });

            NetworkStream dataStreamSend = client2.GetStream();

            dataToSend = Encoding.UTF8.GetBytes(txt_SendData.Text);
            dataStreamSend.Write(dataToSend, 0, dataToSend.Length);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)// server
        {
            TcpListener server = null;
          
            try
            {
                int port = 10000;//Int32.Parse(txt_Port.Text);
                await Task.Factory.StartNew(() =>
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    Dispatcher.Invoke(() =>
                    {
                        txt_Info.Text = "Server has been started";
                        btn_StartServer.IsEnabled = false;
                        
                    });
                    while (true)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            txt_Info.Text += "\nWatiting for client connections";
                            
                        });

                        clientS = server.AcceptTcpClient();
                        Dispatcher.Invoke(() =>
                        {
                            txt_Info.Text += "\nClient is connected";
                        });
                        dataStream = clientS.GetStream();
                        string message = "\nWelcome, you are connected";
                        byte[] reply = Encoding.UTF8.GetBytes(message);
                        dataStream.Write(reply, 0, reply.Length);
                        // или
                        //Socket socket = server.AcceptSocket();
                       

                        Dispatcher.Invoke(() =>
                        {
                            txt_Info.Text += "\nConfirmation meesage sent to the client\n";
                        });
                        do
                        {
                            int bytes = dataStream.Read(dataToReceive, 0, dataToReceive.Length);

                            Dispatcher.Invoke(() =>
                            {
                                txt_SendData.Text += "\n" + response.Append(Encoding.UTF8.GetString(dataToReceive, 0, bytes)).ToString();
                            });
                        }

                        while (dataStream.DataAvailable);
                        clientS.Close();                                  
                    }
                    
                });

            }
            catch (Exception ex)
            {
                txt_Info.Text = $"Something went wrong:\n{ex.Message}";
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }



        }

    }
}
