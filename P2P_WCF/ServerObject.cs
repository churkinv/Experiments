using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2P_WCF
{
    //public delegate void SendMessageToTextBox(string message);

    class ServerObject
    {
        static TcpListener tcpListener; // сервер для прослушивания
        List<ClientObject> clients = new List<ClientObject>(); // все подключения

        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }
        // прослушивание входящих подключений
        public async void Listen(SendMessageToTextBox sendMessage)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                
                Console.OutputEncoding = System.Text.Encoding.UTF8; // if there  is a problems with russians characters
                sendMessage("Сервер запущен. Ожидание подключений...");


                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    //Thread clientThread = new Thread(new ThreadStart(clientObject.Process(send)));
                    //clientThread.Start();

                   //await Task.Factory.StartNew(() => clientObject.Process());
                }
            }
            catch (Exception ex)
            {
                sendMessage(ex.Message);
                Disconnect();
            }
        }
        

        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id) // если id клиента не равно id отправляющего
                {
                    clients[i].Stream.Write(data, 0, data.Length); //передача данных
                }
            }
        }
        // отключение всех клиентов
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }
    }
}
