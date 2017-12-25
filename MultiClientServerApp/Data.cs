using RaccoonsLibraryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.P2P
{
    /// <summary>
    /// Этот класс- это пакет с данным для отправки по сети. Для МVP
    /// </summary>

    [Serializable]
    public class Data /*where T: new()*/
    {
        public Type DataType { get; private set; }
        //public T GenericData { get; private set; }

        public string StringData { get; private set; }
        public object ObjectData { get; private set; }
        public int IntData { get; private set; }
        public float FloatData { get; private set; }
        public byte[] ByteData { get; private set; }
        
        //public Data(T data)
        //{
        //    this.GenericData = data;
        //    DataType = data.GetType();           
        //}

        public Data(string data)
        {
            DataType = data.GetType();
            StringData = data;
            ByteData = data.GetBytes();
        }

        public void CreatePacket()
        {

        } // not finished        
    }
    //public T DataToHandle { get; private set; }

    //public Data(T DataToHandle)
    //{
    //    this.DataToHandle = DataToHandle;
    //    DataType = DataToHandle.GetType().ToString();
    //}

    //public static string GetType()
    //{

    //    string type = Type;

    //    return type;
    //}

}
