using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace NetworkFileDataTransferTCP
{
    
    public partial class MainWindow : Window
    {
        TcpListener listener;
        TcpClient client;
        NetworkStream networkStream;
        byte[] buffer; 
        int bufferSize; 
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_StartServer_1_Click(object sender, RoutedEventArgs e)
        {
            btn_StartServer_1.IsEnabled = false;
            btn_StartServer_2.IsEnabled = false;

            try
            {
                listener = new TcpListener(IPAddress.Any, 10000);
                listener.Start();
                lbl_InforS1.Content = "Server started";

                while (true)
                {
                    client = await listener.AcceptTcpClientAsync();
                    networkStream = client.GetStream();
                    Dispatcher.Invoke(() => {
                        txt_S_1.Text += "Client is connected";
                    });
                   
                    do
                    {
                        IFormatter formatter = new BinaryFormatter();
                        //bytes = networkStream.Read(buffer,0, buffer.Length);

                        //Stream dataStream = new FileStream(@"MyPic.jpg", System.IO.FileMode.OpenOrCreate);
                        buffer = (byte[]) formatter.Deserialize(networkStream);
                        string del = "DEL";// это мой раграничитель для определения расширения файла
                      
                        string type = System.Text.Encoding.UTF8.GetString(buffer); // загоним все в строку
                        int num = type.IndexOf("DEL"); // найдем в нашей строке индекс нашего разграничителя
                        //networkStream.Read(buffer, 0, buffer.Length);
                        type = type.Substring(0, num); // все что до него будут искомые символы
                        txt_S_2.Text += type;
                        using (var fs = new FileStream($"Test2{type}", FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(buffer, 0, buffer.Length);
                        }

                    }
                    while (networkStream.DataAvailable);
                    networkStream.Close();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                lbl_InforS1.Content = "Failed to start";
                txt_S_1.Text = ex.Message;
                listener.Stop();
            }
           
        }

        private void btn_StartServer_2_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btn_SendToS2_Click(object sender, RoutedEventArgs e)
        {

            //using (FileStream fstream = new FileStream(@"‪C:\Users\Raccoon\Desktop\Украина1.jpg", FileMode.OpenOrCreate))
            //{
            // преобразуем картинку в байты
            //byte[] myFile = File.ReadAllBytes(@"‪C:\Users\Raccoon\Desktop\Украина1.jpg");
            // запись массива байтов в файл
            //    fstream.Write(array, 0, array.Length);                
            //}
            //Stream dataStream = new FileStream(@"MyPic.dat", System.IO.FileMode.Create);

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    client = new TcpClient();
                    client.Connect("192.168.0.102", 10000);
                    networkStream = client.GetStream();
                    IFormatter binaryFormatter = new BinaryFormatter();
                    buffer = File.ReadAllBytes(@"E:\Программы\en_r_server_901_for_teradata_x64_9649029.gz");

                    string path = @"E:\Программы\en_r_server_901_for_teradata_x64_9649029.gz";

                    //byte [] size = BitConverter.GetBytes(buffer.Length); //размер массива

                    string getType = System.IO.Path.GetExtension(path); // получаю разширение файла
                    byte[] type = Encoding.UTF8.GetBytes(getType);
                    byte[] delimeter = Encoding.UTF8.GetBytes("DEL");
                    byte[] package = new byte[type.Length + delimeter.Length +buffer.Length]; // формируем массив размером для файла и его типа

                    type.CopyTo(package, 0);
                    delimeter.CopyTo(package, type.Length);
                    buffer.CopyTo(package, type.Length+delimeter.Length);                

                    binaryFormatter.Serialize(networkStream, package);

                    //using (var fs = new FileStream(@"1384502389_1122979572 (1).gif", FileMode.Open))
                    //{
                    //    fs.Read(package,0, package.Length);
                    //    binaryFormatter.Serialize(networkStream, buffer);
                    //}                    
                });               
            }
            catch (Exception ex)
            {
                txt_S_1.Text = ex.Message;
            }

            networkStream.Close();
            client.Close();
        }
    }
}
