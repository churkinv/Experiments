using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Я от бабушки ушел я от  дедушки ушел";
            string[] text2 = { "Я от бабушки ушел я от  дедушки ушел" };
            int count = 0;
            Uri uri = new Uri("https://finance.ua/");
            string [] html = { new WebClient().DownloadString(uri) };

            foreach (string p in html)
            {
                Console.WriteLine(p);
            }

            #region String and StringBuilder
            //С помощью функции Split мы можем разделить строку на массив подстрок. 
            //В качестве параметра функция Split принимает массив символов или строк, которые и будут служить разделителями. 
            //Например, подсчитаем количество слов в сроке, разделив ее по пробельным символам:
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in words)
            {
                count++;
            }
            Console.WriteLine(count);
            #endregion

            #region Регулярные выражения
            Regex regex = new Regex(@" \w*баб\w*"); // паттерн: слова в которых есть "баб", * мы не знаем кол-во, может быть любое
            MatchCollection matches = regex.Matches(text); // вернет коллекцию всех совпадений по нашему паттерну
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value); // выдаст из коллекции значение совпадения
            }

            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"; // паттерн для определения корректности email, c MSDN
            while (true)
            {
                Console.WriteLine("Введите адрес электронной почты");
                string email = Console.ReadLine();

                if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Email подтвержден");
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный email");
                }
            }
            
            //Класс Regex имеет метод Replace, который позволяет заменить строку, 
            //соответствующую регулярному выражению, другой строкой
            //Данная версия метода Replace принимает два параметра: 
            //строку с текстом, где надо выполнить замену, и сама строка замены. 
            //Так как в качестве шаблона выбрано выражение "\s+ (то есть наличие одного и более пробелов), 
            //метод Replace проходит по всему тексту и заменяет несколько подряд идущих пробелов ординарными.
            string s = "Мама  мыла  раму. ";
            string pattern2 = @"\s+";
            string target = " ";
            Regex regex2 = new Regex(pattern2);
            string result = regex.Replace(s, target);


            //Нахождение телефонного номера в формате 111 - 111 - 1111:
            string s2 = "456-435-2318";
            Regex regex3 = new Regex(@"\d{3}-\d{3}-\d{4}"); // в скобках указывается кол-во символов: три

            // Теперь укажем поиск, не зависимый от регистра
            regex = new Regex("World", RegexOptions.IgnoreCase);

            // Console.WriteLine("РегистроНЕзависимый поиск: ");
            foreach (string str in text2)
            {
                if (regex.IsMatch(str))
                    Console.WriteLine("В исходной строке: \"{0}\" есть совпадения!", str);
            }


            #endregion

            Console.WriteLine(text);
            Console.Read();
        }
    }
}
