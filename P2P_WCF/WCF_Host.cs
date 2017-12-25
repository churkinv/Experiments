//using Network.WCF;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Diagnostics;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Net.NetworkInformation;
//using Network;
//using Network.WCF.Services;

//namespace Network.WCF.Host
//{
//    public class WCF_Host : ServiceHost
//    {
//        public ServiceHost a;
//        //public EndpointAddress address;
//        //public Binding binding;
//        //Type serviceType = typeof(NetworkDiscovery);
//        //Uri[] baseAddresses = new String() {"net.tcp://192.168.0.101:20000/NetworkDiscovery" };

//        public WCF_Host (Type serviceType, params Uri[] baseAddresses)
//              : base(serviceType, baseAddresses)
//        {
//            Console.WriteLine("MyServiceHost Constructor"); 

//        }

//        protected override void ApplyConfiguration()
//        {
//            string straddress = GetAddress();
//            Uri address = new Uri(straddress);
//            Binding binding = GetBinding();
//            Type contract = GetContract();
//            base.AddServiceEndpoint(contract, binding, address);// вместо контракт было typeof(iData)
//        }

//        public void HostStart()
//        {
//            ApplyConfiguration();
//            a = new ServiceHost(typeof(NetworkDiscovery)); // Объявляем сервис хост.

//            // хост без конфигурации, т.е. прописываем данные кодом
//            //address = new EndpointAddress("net.tcp://192.168.0.101:20000/NetworkDiscovery");
//            //binding = new NetTcpBinding();
//            //contract = typeof(INetworkDiscoveryService);
//            ////hostNetworkDiscovery.AddServiceEndpoint(contract, binding, address);// это для хоста без конфигурации, т.е. в апп конфиг сервис модел нет или задокументирован эти строчки должны быть до открытия Хоста. В веб хосте вносим это в Кастом Хост фактори, аналогично
//            //a.AddServiceEndpoint(contract, binding, address.ToString());
//            try
//            {
//                a.Open();
               
//            }
//            catch (Exception Ex)
//            {
//                MessageBox.Show($"Трапилось щось незрозуміле.Помилка: {Ex}"); 
//            }
                                                
//        }

//        public string HostClose()
//        {
//            string Message;
//            try
//            {
//                a.Close();
//                Message = "Closed succesfully";
//            }
//            catch (Exception Ex)
//            {
//                Message = $"Трапилось щось незрозуміле.Помилка: {Ex}"; 
//            }
            
//            return Message;
//        }

//        public string GetAddress()
//        {
//            return "net.tcp://192.168.0.101:20000/NetworkDiscovery";
//        }

//        public Binding GetBinding()
//        {
//            NetTcpBinding binding = new NetTcpBinding();
//            binding.Security.Mode = SecurityMode.None;
//            return binding;
//        }

//        public Type GetContract()
//        {
//            Type contract = typeof(INetworkDiscoveryService);

//            return contract;
//        }
//    }
//}

