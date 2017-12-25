using Network.P2P;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiClientServerApp.Event
{
    /// <summary>
    /// Ивент возникающий при получении данных.
    /// </summary>
    internal class DataTransferedEventArgs : EventArgs
    {
        private readonly Data data;

        public DataTransferedEventArgs(Data data)
        {
            this.data = data;
        }
        
        public Data Data
        {
            get { return this.data; }           
        }

    }

    /// <summary>
    ///  Метод расширения, инкапсулирующий логику, 
    /// безопасную в отношении потоков.
    /// </summary>
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            // Копирование ссылки на поле делегата во временное поле
            // для безопасности в отношении потоков
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
            // Если зарегистрированный метод заинтересован в событии, уведомите его
            if (temp != null) temp(sender, e);
        }
    }

}
