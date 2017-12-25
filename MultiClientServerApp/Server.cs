using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MultiClientServerApp.Event;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using RaccoonsLibraryCommon;

namespace Network.P2P
{
    /// <summary>
    /// Делегат для обработки сообщений из методов. 
    /// Delegate for handling messages from methods
    /// </summary>
    /// <param name="message"></param>
    public delegate void MessageHandler(string message);


    /// <summary>
    /// Класс для запуска сервера
    /// Server
    /// </summary>
    internal class Server
    {
        public event EventHandler<DataTransferedEventArgs> NewData;
        public static event MessageHandler MessageToPass;

        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        private static TcpListener _tcpListener;
        private NetworkStream _networkStream;
        private CompanyNetwork _companyNetwork;
        private List<Client> _clients = new List<Client>();
        private Task t;

        private IPAddress _iPAddress;
        private readonly int _port;

        internal int Port
        {
            get { return this._port; }
        }
        internal IPAddress IPAddress
        {
            get { return this._iPAddress; }
        }

        public Server(CompanyNetwork companyNetwork) // если сеть создана
        {
            this._companyNetwork = companyNetwork;
            _iPAddress = IPAddress.Any;//NetworkCooperation.GetMyLocalIPv4();
            _port = 12000;
        }

        public Server(IPAddress ip, Int32 port) // для тестирования
        {
            _iPAddress = ip;
            _port = port;
            _tcpListener = new TcpListener(_iPAddress, _port);
        }

        protected internal void AddConnection(Client client)
        {
            _clients.Add(client);
        }

        protected internal void RemoveConnection(string id)
        {
            Client client = _clients.FirstOrDefault(c => c.Peer.Id == id);
            if (client != null)
                _clients.Remove(client);
        }

        // Accept one client connection asynchronously.
        public void DoBeginAcceptTcpClient()
        {
            // Set the event to nonsignaled state.
            tcpClientConnected.Reset();

            // Start to listen for connections from a client.

            // Accept the connection. 
            // BeginAcceptSocket() creates the accepted socket.
            _tcpListener.BeginAcceptTcpClient(
                new AsyncCallback(DoAcceptTcpClientCallback),
                _tcpListener);

            // Wait until a connection is made and processed before 
            // continuing.
            tcpClientConnected.WaitOne();
        }

        // Process the client connection.
        public void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            _tcpListener = (TcpListener)ar.AsyncState;

            // End the operation and display the received data 

            TcpClient tcpClient = _tcpListener.EndAcceptTcpClient(ar);
            // Process the connection here. (Add the client to a
            // server table, read data, etc.)

            _networkStream = tcpClient.GetStream();
            
            try
            {
                string name; 
                byte[] data = DataTransfer.GetData(_networkStream);
                name = data.GetString();
                Client client = new Client(name, tcpClient, this);
                MessageToPass(client.Name);
            }
            catch (Exception ex)
            {
                MessageToPass($"помилка на сервері при отриманні даних!!!:\n {ex}");
            }

            //_tcpClient.Close();
            // Signal the calling thread to continue.
            tcpClientConnected.Set();
        }

        public async Task ListenAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    _tcpListener.Start();

                    MessageToPass("Сервер гарненько працює");

                    try
                    {
                        DoBeginAcceptTcpClient();
                    }
                    catch (SocketException e)
                    {
                        MessageToPass("Сервер зупинено з ексепшеном");
                    }
                }
                catch (Exception e)
                {
                    MessageToPass($"Помилка з'єднання:\n {e}");
                }
                finally
                {
                    //if (_tcpListener != null)
                    //    Disconnect();
                }
            });
        }

        public void Send()
        { }

        /// Callback for Read operation
        //public void ConnectToServer()
        //{
        //    try
        //    {
        //        tcpClient = new TcpClient(AddressFamily.InterNetwork);

        //        IPAddress[] remoteHost = Dns.GetHostAddresses("hostaddress");

        //        //Start the async connect operation           

        //        tcpClient.BeginConnect(remoteHost, portno, new
        //                      AsyncCallback(ConnectCallback), tcpClient);

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(LogLevel.Error, "ex.Message);
        //         }
        //}       

        //private void ConnectCallback(IAsyncResult result)
        //{
        //    try
        //    {
        //        //We are connected successfully.

        //        NetworkStream networkStream = tcpClient.GetStream();

        //        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

        //        //Now we are connected start asyn read operation.

        //        networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
        //    }
        //      Catch(Exception ex)
        //      {
        //        Logger.WriteLog(LogLevel.Error, "ex.Message);
        //       }
        //}

        //private void ReadCallback(IAsyncResult result)
        //{

        //    NetworkStream networkStream;

        //    try
        //    {

        //        networkStream = tcpClient.GetStream();

        //    }

        //    catch
        //    {
        //        Logger.WriteLog(LogLevel.Warning, "ex.Message);
        //     return;

        //    }

        //    byte[] buffer = result.AsyncState as byte[];

        //    string data = ASCIIEncoding.ASCII.GetString(buffer, 0, buffer.Length);

        //    //Do something with the data object here.

        //    //Then start reading from the network again.

        //    networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);

        //}


        // Если этот класс изолированный, нужно сделать этот метод закрытым
        // или невиртуальным

        protected virtual void OnNewData(DataTransferedEventArgs e)
        {
            e.Raise(this, ref NewData);
        }

        public void DataReceived(Data data)
        {
            //TODO logic for where to send data
            DataTransferedEventArgs e = new DataTransferedEventArgs(data);
            OnNewData(e);
        }

        public void Disconnect()
        {
            if (_tcpListener != null)
                _tcpListener.Stop();

            MessageToPass("Сервер зупинено");

            //for (int i = 0; i < clients.Count; i++)
            //{
            //    clients[i].Close(); //отключение клиента
            //}
            //Environment.Exit(0); //завершение процесса
        }
    }
}

