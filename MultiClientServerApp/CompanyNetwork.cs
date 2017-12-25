using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

namespace Network.P2P
{
    /// <summary>
    /// Класс которые отвечает за сеть в которое работает экземпляр приложения
    /// Это "облако" компании
    /// </summary>
    [Serializable]
    public class CompanyNetwork
    {
        public static event MessageHandler MessageToPass;

        public string Name { get; private set; }
        public List <Peer> Members { get; private set; }
        public string KeyWord { get; private set; }
        
        public CompanyNetwork (string name, string keyWord) // добавить шифрование 
        {
            Name = name;
            this.KeyWord = keyWord;
            Members = new List<Peer>();            
        }

        public CompanyNetwork(string name, string keyWord, List <Peer> Members)// когда восстанавливаем из БД, добавить шифрование
        {
            Name = name;
            this.KeyWord = keyWord;
            this.Members = Members;
        }

        public void AddPeer(Peer name/*, string keyWord*/) // добавить шифрование 
        {
            //if (keyWord == KeyWord)
                Members.Add(name);
            //else
            //    OnMessageHandler($"Ви неправильне ввели слово-ідентефікатор мережі.\n Будь ласка запитайте у відповідальної особи");
        }
        
        public void RemovePeer(Peer name)
        {
            // додати функціонал видалення піра тільки уповноваженною особою
            //if (MessageBox.Show($"Ви впевнені що бажаєти видалити {name} з мережі? Він не зможе більше приймати участь в бізнес-процессах.", 
            //    "Видалення одно з учасників мережі", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //{
               
            //}
            //else
            //{
                  Members.Remove(name);
            //    OnMessageHandler($"{name} був видалений з бізнес мережі.\n Додайте його знову аби він міг прймати участь в бізне-процессах компанії");
            //}             
        }

        public void AdjustName(string name, string keyWord)
        {
            //{
            //    if (keyWord == KeyWord)
                    this.Name = name;
            //    else
            //        OnMessageHandler($"Ви неправильне ввели слово-ідентефікатор мережі.\n Будь ласка запитайте у відповідальної особи");
            //}
        }     
      
    }
}
