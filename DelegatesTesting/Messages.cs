using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DelegatesTesting
{ 

    public class Messages
    {
        public delegate void SendMessageToTextBox(string messageInput);
        public event SendMessageToTextBox MessageHandler;

        public void Message()
        {
            MainWindow.t.Text = "\nAbbagalaMaga"; // передаем значение в маин виндов тем самым меняем значение текст бокса
        }

        public void TCPserver()
        {

            Console.WriteLine("message");


            MessageHandler("message");


            TcpListener server = new TcpListener(IPAddress.Any, 20000);
            server.Start();
            string mes1 = "\nServer started";
            MessageHandler(mes1);

            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            string mes2 = "\nWaiting for data from stream";
            MessageHandler(mes2);
            do
            {
                byte[] buffer = new byte[256];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                StringBuilder builder = new StringBuilder();
                builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                MessageHandler("\n"+builder.ToString());
                //MainWindow.t.Text = builder.ToString();
            }

            while (stream.DataAvailable);

            server.Stop();
        }

        public void TCPclient()
        {
            TcpClient client = new TcpClient();
            client.Connect("192.168.0.101", 20000);
            NetworkStream stream = client.GetStream();
            string m = "hello from clietn";
            byte[] bufer2 = Encoding.UTF8.GetBytes(m);
            stream.Write(bufer2, 0, bufer2.Length);

        }

    }
}
