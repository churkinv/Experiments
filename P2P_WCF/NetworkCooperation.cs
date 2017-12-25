using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Threading;
using P2P_WCF;

namespace Network
{
    public class NetworkCooperation
    {
        #region Декларирую properties для идентификации машины в сети (для дальнейшой проверки). Готово.

        public static IPAddress _localIP { get; protected set; }
        public static IPAddress _IPv6 { get; protected set; } 
        public static IPAddress _gatewayIP { get; protected set; }
        public static string _externalIP { get; protected set; } 
        public static IPEndPoint _myPublicEPStun { get; protected set; }

        #endregion

        //public (List <string>, List<IPAddress>) GetAllDevices() // эксперимент с использованием Tuples, возврат всех активных машин сети
        //{
        //    var _allDevicesIP = new List <string>();
        //    var _allDevicesNames = new List<IPAddress>();

        //    Ping _myPing = new Ping();
        //    foreach (string adresses in _allDevicesIP)
        //    {
        //        IPAddress address = IPAddress.Parse(adresses);
        //        PingReply reply = _myPing.Send (adress);
        //    }
        //    return (_allDevicesIP, _allDevicesNames) ; 
        //}

        public static IPAddress GetMyLocalIPv4() // метод для получение локального IPv4
        {
            IPAddress[] _localIPList = Dns.GetHostAddresses(Dns.GetHostName()); // получае полный список всех айпи адресов данной машины (хоста) - v4+v6

            foreach (IPAddress a in _localIPList) // прохожусь по все адресам
            {
                if (a.AddressFamily == AddressFamily.InterNetwork)// отфильтровую локальный IP4 (InterNetworkv6 для версии 6)
                    _localIP = a;// записываю то что совпало в переменную
            }
            return _localIP;
        }

        public static IPAddress GetMyLocalIPv6() // метод для получение локального(?) IPv6
        {
            IPAddress[] _localIPList = Dns.GetHostAddresses(Dns.GetHostName()); // получае полный список всех айпи адресов 
                                                                                //данной машины (хоста) - v4+v6
            foreach (IPAddress a in _localIPList) // прохожусь по все адресам
            {
                if (a.AddressFamily == AddressFamily.InterNetworkV6)// отфильтровую локальный IP6
                    _IPv6 = a;// записываю то что совпало в переменную
            }
            return _IPv6;
        }

        public static string GetExtarnalIP() // метод для получение внешнего IPv4
        {
            _externalIP = new WebClient().DownloadString("http://icanhazip.com"); // получаем свой внешний айпи из внешнего источника // добавить возможность внести свой источник проверки внешнего айпи
            return _externalIP; // важно при отсутствии сети, выдаст ексепшн --> обработать

        }

        public static IPAddress GetMyGatewayIP() // метод для получение адреса роутера через который выходим в интернет IPv4
        {
            _gatewayIP = NetworkInterface.GetAllNetworkInterfaces() // другой способ через линк запрос по условиям, по идее так же можно найти и способом выше
              .Where(n => n.OperationalStatus == OperationalStatus.Up)
              .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
              .Select(g => g?.Address)
              .FirstOrDefault(a => a != null); // получаем дефолтный GateWay или первый
            return _gatewayIP;
        }

        public static IPEndPoint GetMyPublicEP() // метод для получение нашего внешнего EndPoint, через Стан сервера, UDP
        {

            // Create new socket for STUN client.
            Socket _socket = new Socket
                (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(new IPEndPoint(IPAddress.Any, 0));

            List<string> _stunServers = new List<string> { // список стан серверов для проверки своей ендпоинт, не все работают
                "stun.l.google.com:19302",
                "stun1.l.google.com:19302",
                "stun2.l.google.com:19302",
                "stun3.l.google.com:19302",
                "stun4.l.google.com:19302",
                "stun01.sipphone.com",
                "stun.ekiga.net",
                "stun.fwdnet.net",
                "stun.ideasip.com",
                "stun.iptel.org",
                "stun.rixtelecom.se",
                "stun.schlund.de",
                "stunserver.org",
                "stun.softjoys.com",
                "stun.voiparound.com",
                "stun.voipbuster.com",
                "stun.voipstunt.com",
                "stun.voxgratia.org",
                "stun.xten.com"
            };

            foreach (string server in _stunServers)
            {
                try
                {
                    STUN_Result result = STUN_Client.Query(server, 3478, _socket);
                    IPEndPoint myPublicEPStun = result.PublicEndPoint;

                    if (myPublicEPStun != null)
                    {
                        _myPublicEPStun = myPublicEPStun;
                        break;
                    }
                }
                catch (Exception E)
                {
                    Console.WriteLine("Якась необ`ясніма магія трапилась");
                }
            }
            return _myPublicEPStun;
        }

        public static List<string> GetAllLocalDevices()
        {
            Ping _myPing;
            PingReply _myPingReply;
            IPAddress _ipAddress;
            IPHostEntry _ipHostEntry;
            List<string> _localDevices = new List<string>();

            for (int i = 1; i < 255; i++)
            {
                string _subnet = "." + i.ToString();
                _myPing = new Ping();
                _myPingReply = _myPing.Send("192.168.0" + _subnet); // добавить многопоточность + автоматически определять маску локальной подсети

                if (_myPingReply.Status == IPStatus.Success)
                {
                    try
                    {
                        _ipAddress = IPAddress.Parse("192.168.0" + _subnet);
                        //_ipHostEntry = Dns.GetHostEntry(_ipAddress); // выдает ошибку 
                        _localDevices.Add("192.168.0" + _subnet + "-" /*+ _ipHostEntry.ToString()*/);
                    }

                    catch (Exception Ex)
                    {
                        System.Windows.MessageBox.Show(Ex.ToString());
                    }
                }
            }

            return _localDevices;
        } // метод для получения активных айпи адресов локальной сети неоптимизирован НЕ ГОТОВ но работает

        public static IPAddress GetSubnetMask()  // метод получения маски подсети НЕ ГОТОВ
        {
            IPAddress _mask = null;
            return _mask;
        }

        public static IPAddress GetMyNetworkBroadcast() // метод получения бродкаста сети в которой работает машина НЕ ГОТОВ
        {
            IPAddress _broadcastIP = null;
            return _broadcastIP;
        }

        public static void ShowNetworkInterfaces() // MSDN  метод показывающий информацию об сетевых устройствах, пример с мсдн, но нет двух методов-  один ниже, но в данном методе он принимает два параметра, перегрузки нет?
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
                return;
            }

            Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine();
                Console.WriteLine(adapter.Description);
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                Console.WriteLine("  Physical Address ........................ : {0}",
                           adapter.GetPhysicalAddress().ToString());
                Console.WriteLine("  Operational status ...................... : {0}",
                    adapter.OperationalStatus);
                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
                Console.WriteLine("  IP version .............................. : {0}", versions);
                ShowIPAddresses(properties); 

                // The following information is not useful for loopback adapters.
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                Console.WriteLine("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);

                string label;
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
                    Console.WriteLine("  MTU...................................... : {0}", ipv4.Mtu);
                    if (ipv4.UsesWins)
                    {

                        IPAddressCollection winsServers = properties.WinsServersAddresses;
                        if (winsServers.Count > 0)
                        {
                            label = "  WINS Servers ............................ :";
                            //ShowIPAddresses(label, winsServers);
                        }
                    }
                }

                Console.WriteLine("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);
                Console.WriteLine("  Receive Only ............................ : {0}",
                    adapter.IsReceiveOnly);
                Console.WriteLine("  Multicast ............................... : {0}",
                    adapter.SupportsMulticast);
                //ShowInterfaceStatistics(adapter);

                Console.WriteLine();
            }

        }

        public static void ShowIPAddresses(IPInterfaceProperties adapterProperties) // MSDN The following code example displays address information. MSDN - по идее этот метод для метода выше ShowNetworkInterfaces() 
        {
            IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
            if (dnsServers != null)
            {
                foreach (IPAddress dns in dnsServers)
                {
                    Console.WriteLine("  DNS Servers ............................. : {0}",
                        dns.ToString()
                   );
                }
            }
            IPAddressInformationCollection anyCast = adapterProperties.AnycastAddresses;
            if (anyCast != null)
            {
                foreach (IPAddressInformation any in anyCast)
                {
                    Console.WriteLine("  Anycast Address .......................... : {0} {1} {2}",
                        any.Address,
                        any.IsTransient ? "Transient" : "",
                        any.IsDnsEligible ? "DNS Eligible" : ""
                    );
                }
                Console.WriteLine();
            }

            MulticastIPAddressInformationCollection multiCast = adapterProperties.MulticastAddresses;
            if (multiCast != null)
            {
                foreach (IPAddressInformation multi in multiCast)
                {
                    Console.WriteLine("  Multicast Address ....................... : {0} {1} {2}",
                        multi.Address,
                        multi.IsTransient ? "Transient" : "",
                        multi.IsDnsEligible ? "DNS Eligible" : ""
                    );
                }
                Console.WriteLine();
            }
            UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;
            if (uniCast != null)
            {
                string lifeTimeFormat = "dddd, MMMM dd, yyyy  hh:mm:ss tt";
                foreach (UnicastIPAddressInformation uni in uniCast)
                {
                    DateTime when;

                    Console.WriteLine("  Unicast Address ......................... : {0}", uni.Address);
                    Console.WriteLine("     Prefix Origin ........................ : {0}", uni.PrefixOrigin);
                    Console.WriteLine("     Suffix Origin ........................ : {0}", uni.SuffixOrigin);
                    Console.WriteLine("     Duplicate Address Detection .......... : {0}",
                        uni.DuplicateAddressDetectionState);

                    // Format the lifetimes as Sunday, February 16, 2003 11:33:44 PM
                    // if en-us is the current culture.

                    // Calculate the date and time at the end of the lifetimes.    
                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressValidLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     Valid Life Time ...................... : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );
                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressPreferredLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     Preferred life time .................. : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );

                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.DhcpLeaseLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     DHCP Leased Life Time ................ : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );
                }
                Console.WriteLine();
            }
        }

        public static void DisplayIPv4NetworkInterfaces() //MSDN
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("IPv4 interface information for {0}.{1}",
               properties.HostName, properties.DomainName);
            Console.WriteLine();

            foreach (NetworkInterface adapter in nics)
            {
                // Only display informatin for interfaces that support IPv4.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4) == false)
                {
                    continue;
                }
                Console.WriteLine(adapter.Description);
                // Underline the description.
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                // Try to get the IPv4 interface properties.
                IPv4InterfaceProperties p = adapterProperties.GetIPv4Properties();

                if (p == null)
                {
                    Console.WriteLine("No IPv4 information is available for this interface.");
                    Console.WriteLine();
                    continue;
                }
                // Display the IPv4 specific data.
                Console.WriteLine("  Index ............................. : {0}", p.Index);
                Console.WriteLine("  MTU ............................... : {0}", p.Mtu);
                Console.WriteLine("  APIPA active....................... : {0}",
                    p.IsAutomaticPrivateAddressingActive);
                Console.WriteLine("  APIPA enabled...................... : {0}",
                    p.IsAutomaticPrivateAddressingEnabled);
                Console.WriteLine("  Forwarding enabled................. : {0}",
                    p.IsForwardingEnabled);
                Console.WriteLine("  Uses WINS ......................... : {0}",
                    p.UsesWins);
                Console.WriteLine();
            }
        }

        public static void DisplayIPv6NetworkInterfaces() //MSDN
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("IPv6 interface information for {0}.{1}",
               properties.HostName, properties.DomainName);

            int count = 0;

            foreach (NetworkInterface adapter in nics)
            {
                // Only display informatin for interfaces that support IPv6.
                if (adapter.Supports(NetworkInterfaceComponent.IPv6) == false)
                {
                    continue;
                }

                count++;

                Console.WriteLine();
                Console.WriteLine(adapter.Description);
                // Underline the description.
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));

                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                // Try to get the IPv6 interface properties.
                IPv6InterfaceProperties p = adapterProperties.GetIPv6Properties();


                if (p == null)
                {
                    Console.WriteLine("No IPv6 information is available for this interface.");
                    Console.WriteLine();
                    continue;
                }
                // Display the IPv6 specific data.
                Console.WriteLine("  Index ............................. : {0}", p.Index);
                Console.WriteLine("  MTU ............................... : {0}", p.Mtu);
            }

            if (count == 0)
            {
                Console.WriteLine("  No IPv6 interfaces were found.");
                Console.WriteLine();
            }

        }

        public static void TcpConnectionServer(Int32 port/*, IPAddress myIp*/)
        {

            TcpListener _server = null;
            try
            {
                // Set the TcpListener on port
                Int32 _port = port;
                IPAddress _myIp = IPAddress.Any;//myIp; //GetMyLocalIPv4(); // or IPAddress.Parse ("168.198.0.1")

                // TcpListener server = new TcpListener(port);
                _server = new TcpListener(_myIp, _port);

                // Start listening for client requests.
                _server.Start();

                // Buffer for reading data
                Byte[] _bytes = new Byte[2000];
                String _data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for connection...");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient _client = _server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    _data = null;
                    // Get a stream object for reading and writing
                    NetworkStream _stream = _client.GetStream();

                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = _stream.Read(_bytes, 0, _bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        _data = System.Text.Encoding.ASCII.GetString(_bytes, 0, i);
                        //Console.WriteLine($"Received: {_data}");

                        // Process the data sent by the client.
                        //_data = _data.ToUpper();

                        //byte[] _msg = System.Text.Encoding.ASCII.GetBytes(_data);

                        // Send back a response.
                        //_stream.Write(_msg, 0, _msg.Length);
                        Console.WriteLine($"Sent: {_data}");
                    }
                    // Shutdown and end connection
                    _client.Close();
                }
            }
            catch (SocketException e)
            {
                System.Windows.MessageBox.Show($"SocketException: {e}");
            }
            finally
            {
                // Stop listening for new clients.
                _server.Stop();

            }

            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to close this window?", "Confirmation", 
                                                                      MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //Total nn = new Total(result);
                System.Windows.Application myApp = new System.Windows.Application();
                myApp.Shutdown();
                   
            }
             
        }// MSDN метод запуска TCP сервера, работает (необходима доработка).

        public static void SocketServerTCP(int _port, string ip)
        {
            int port = _port; // порт для приема входящих запросов
           
                // получаем адреса для запуска сокета
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

                // создаем сокет
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    // связываем сокет с локальной точкой, по которой будем принимать данные
                    listenSocket.Bind(ipPoint);

                    // начинаем прослушивание
                    listenSocket.Listen(10);

                    Console.WriteLine("Server is running. Waiting for connection");

                    while (true)
                    {
                        Socket handler = listenSocket.Accept();
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[256]; // буфер для получаемых данных

                        do
                        {
                            bytes = handler.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (handler.Available > 0);

                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                        // отправляем ответ
                        string message = "Your message is";
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } // для полноты картины

        public static void TcpConnectionClient(String _server, Int32 port, String _message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 _port = port; //15000;
                TcpClient _client = new TcpClient(_server,_port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] _data = System.Text.Encoding.ASCII.GetBytes(_message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                NetworkStream stream = _client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(_data, 0, _data.Length);

                Console.WriteLine($"Sent: {_message}");

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                _data = new Byte[256];

                // String to store the response ASCII representation.
                String _responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 _bytes = stream.Read(_data, 0, _data.Length);
                _responseData = System.Text.Encoding.ASCII.GetString(_data, 0, _bytes);
                Console.WriteLine($"Received: {_responseData}");

                // Close everything.
                stream.Close();
                _client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"ArgumentNullException: {e}");
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
            }

            Console.WriteLine($"\n Press Enter to continue");
            Console.Read();


        } // MSDN, метод запуска ТСP клиента в сервер передаем айпиадрес, работает (необходима доработка).

        public static void SocketClientTCP(int _port, string ip)
        {
            // адрес и порт сервера, к которому будем подключаться
            int port = _port; // порт сервера
            string address = ip;//"127.0.0.1"; // адрес сервера
            try
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);
                    Console.Write("Enter message:");
                    string message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("Server reply: " + builder.ToString());

                    // закрываем сокет
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.Read();

            } // для полноты картины

        public static void SendFile(string path, Int32 port, IPAddress serverIP) // failed in test
        {
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = serverIP;//ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // Create a TCP socket.
            Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint.
            client.Connect(ipEndPoint);

            // There is a text file test.txt located in the root directory.
            string fileName = path;

            // Send file fileName to remote device
            Console.WriteLine("Sending {0} to the host.", fileName);
            client.SendFile(fileName);

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();

        }

    }
}
