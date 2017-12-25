using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Network.P2P
{
    public delegate void getData (NetworkStream stream);

    /// <summary>
    /// Класс отвечающий за отправку и получение пакетов по сети.
    /// Он сериализует и десириализует полученные данные в и из сетевого потока.
    /// Class responsible for data transfer. 
    /// </summary>
    public static class DataTransfer 
    {
        public static event MessageHandler MessageToPass;
        private static IFormatter binaryFormatter = new BinaryFormatter();        

        //public static void SendData <T> (T data, NetworkStream stream)
        //{
        //    Data<T> dataToSend = new Data<T>(data);           
            
        //    try
        //    {
        //        binaryFormatter.Serialize(stream, dataToSend); // исправить эексепшен с сериализаицей напрямую в поток. Все работает, но нужно исправить
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageToPass($"Помилка під час відправлення даних:\n{ex}");
        //    }            
        //}

        public static void SendData (byte[] data, NetworkStream stream)
        {
            try
            {
                binaryFormatter.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                MessageToPass($"Помилка під час відправлення даних:\n{ex}");
            }            
        }

        public static byte[] GetData (NetworkStream stream)
        {
            byte[] data = null;

            try
            {
                data = (byte[])binaryFormatter.Deserialize(stream);
                
            }
            catch (Exception ex)
            {
                MessageToPass($"Помилка під час отримання даних:\n{ex}");
            }

            return data;
        }

        //public static Data<T> GetData <T> (NetworkStream stream)
        //{
        //    T data;
        //    Data<T> dataToGet = null;

        //    try
        //    {
        //        data = (T)binaryFormatter.Deserialize(stream);
        //        dataToGet = new Data<T>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageToPass($"Помилка під час отримання даних:\n{ex}");
        //    }           

        //    return dataToGet;          
        //}
        
        public static Peer GetPeer(NetworkStream stream)
        {
            Peer data;

            data = (Peer)binaryFormatter.Deserialize(stream);

            return data;
        }

        public static string GetString(NetworkStream stream)
        {
            string data;

            data = (string)binaryFormatter.Deserialize(stream);

            return data;
        }

        public static EventArgs EventArgs(NetworkStream stream)
        {
            EventArgs OnSomethingHappend = null;
            
            return OnSomethingHappend; 
        }               

        #region For Refactoring

        #region Old version
        //private static byte[] buffer; // буфер которые будет принимать данные переданные в метод
        //private static byte[] type = new byte[32]; // записывается тип данных которые сериализуются
        //private static byte[] delimeter = Encoding.UTF8.GetBytes("/*D*/"); // разграничитель в пакете
        //private static byte[] package; //финальный пакет для отправки 
        #endregion


        /// <summary>
        /// Не ИСПОЛЬЗОВАТЬ. это эксперимент
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        //public static void SendData<T>(IEnumerable<T> data, NetworkStream stream)
        //{
        //    //buffer = (IEnumerable< out T>) data.ToArray;
        //    //IEnumerable<> decimalValues = data.Select((byte) v => (byte)Convert.ToByte(v));

        //    type = Encoding.UTF8.GetBytes("UNKOWN");

        //    type.CopyTo(package, 0);
        //    delimeter.CopyTo(package, type.Length);
        //    buffer.CopyTo(package, type.Length + delimeter.Length);

        //    binaryFormatter.Serialize(stream, package);
        //}

        //byte [] size = BitConverter.GetBytes(buffer.Length); //размер массива
        //private static string getType = System.IO.Path.GetExtension(path); // получаю разширение файла

        //BinaryFormatter bf = new BinaryFormatter();
        //using (var ms = new MemoryStream()) // через грабли но что бы все отправить одним массивом отпраляю объект в виде массива, т.е. буде две сериализации
        //{
        //    bf.Serialize(ms, data);
        //    buffer = ms.ToArray();
        //


        //public static void SendData(string message, NetworkStream stream)
        //{
        //    try
        //    {
        //        buffer = Encoding.UTF8.GetBytes(message);
        //        type = Encoding.UTF8.GetBytes("string");

        //        type.CopyTo(package, 0);
        //        //delimeter.CopyTo(package, type.Length);
        //        buffer.CopyTo(package, type.Length + delimeter.Length);

        //        binaryFormatter.Serialize(stream, package);
        //        binaryFormatter.Serialize(stream, buffer);
        //    }
        //    catch (Exception ex)
        //    {
        //        OnMessageHandler($"Помилка під час відправлення даних від сервера:\n{ex}");
        //    }
        //}

        //public static void SendData(byte[] data, NetworkStream stream)
        //{
        //    try
        //    {
        //        buffer = data;
        //        type = Encoding.UTF8.GetBytes("Byte");

        //        type.CopyTo(package, 0);
        //        //delimeter.CopyTo(package, type.Length);
        //        //buffer.CopyTo(package, type.Length + delimeter.Length);

        //        binaryFormatter.Serialize(stream, package);
        //        binaryFormatter.Serialize(stream, buffer);
        //    }
        //    catch (Exception ex)
        //    {
        //        OnMessageHandler($"Помилка під час відправлення даних від сервера:\n{ex}");
        //    }

        //}


        #endregion
    }
}
