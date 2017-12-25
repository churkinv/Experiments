using MultiClientServerApp.Event;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Network.P2P
{/// <summary>
/// Класс отвечающий за подключение к серверу клиента, а также коммуникацию с клиентом и пирами.
/// </summary>
    internal class Client : IDisposable
    {
        public static event MessageHandler MessageToPass; // Ивент возникающей при необходимости получить или передать сообщение

        private NetworkStream _stream;
        private TcpClient _client;
        public Peer Peer { get; private set; }
        private Server _server;
        public string Name { get; private set; }

        //private Receiver _receiver;
        //private Sender _sender;

        public Client(string name, TcpClient tcpClient, Server server)
        {
            //Peer = peer;
            Name = name; 
            _client = tcpClient;
            _server = server;
            server.AddConnection(this);
       
            server.NewData += ReceivedData;
        }

        private void ReceivedData(Object sender, DataTransferedEventArgs e)
        {
            // 'sender' используется для взаимодействия с объектом MailManager,
            // если потребуется передать ему какую-то информацию
            // 'e' определяет дополнительную информацию о событии,
            // которую пожелает предоставить MailManager
            // Обычно расположенный здесь код отправляет сообщение по факсу
            // Тестовая реализация выводит информацию на консоль
            MessageToPass("We recived some data" + e.Data.ToString());
            //Console.WriteLine(" From={0}, To={1}, Subject={2}",
            //e.Data, e., e.Subject);
        }

        public async void Connect(IPAddress server/*, int port, String data*/) // для MVP
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    _client.Connect(server, 12000);
                    do                    {
                        _stream = _client.GetStream();
                    }
                    while (_stream.DataAvailable);
                });
            }

            catch (Exception ex)
            {
                MessageToPass($"Помилка на стороні клієнта:\n {ex}");
            }
            finally
            {
                Close();
            }
        }

        protected internal static void Close()
        {
            //if (_stream != null)
            //    _stream.Close();
            //if (_client != null)
            //    _client.Close();
        }

        public void Dispose()
        {
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }


    }
}
