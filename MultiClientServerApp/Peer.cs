using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Network.P2P
{
    /// <summary>
    /// Это экземпляр программы, другими словами компьютер на котором запущена программа. 
    /// Он же пользователь. В дальнейшем переписать его под сотрудника. Данная версия для MVP.
    /// </summary>
    [Serializable]
    public class Peer
    {
        public static event MessageHandler OnMessageHandler; // Ивент возникающей при необходимости получить или передать сообщение

        public string Id { get; private set; }
        public IPAddress IpAddress { get; private set; }
        public string UserName { get; private set; }

        public Peer(string name) 
        {
            Id = Guid.NewGuid().ToString();
            IpAddress = NetworkCooperation.GetMyLocalIPv4();
            UserName = name;           
        }

        public Peer(string Id, string name) // на случай получения из БД
        {
            this.Id = Id;
            this.UserName = name;
            IpAddress = NetworkCooperation.GetMyLocalIPv4();
        }
    }
}

