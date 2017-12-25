using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq;
using System.Linq;

namespace RaccoonsLibraryCommon
{
    public static class StringHandler
    {      

        /// <summary>
        /// This method will insert space, in case of absence, in front of capitalized letter.
        /// Этот метод вставляет пробел, в случае отсутствия, перед заглавной буквой.
        /// Example: ScrewDriver->Screw Driver
        /// </summary>
        public static string InsertSpaces(this string source)
        {
            string final = string.Empty;

            if (!String.IsNullOrWhiteSpace(source))
            {
                foreach (char letter in source)
                {
                    if (Char.IsUpper(letter))
                    {
                        final = final.Trim();
                        final += " ";
                    }
                    final += letter;
                }
                final = final.Trim();
            }
            return final;
        }

        /// <summary>
        /// Convert string to Byte Array. Without encoding!
        /// Конвертирует строку в байтовый массив. Без кодировки!
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string source)
        {
            byte[] bytes = new byte[source.Length * sizeof(char)];
            System.Buffer.BlockCopy(source.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Convert Byte Array to string. Without encoding!
        /// Конвертирует байтовый массив в строку. Без кодировки!
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes)
        {
            string str = "";
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            foreach (var item in chars)
            {
                str += item;
            }
            return str;
            //return new string(chars);
        }

        /// <summary>
        /// Convert string to Byte Array with encoding, pass Encoding type by string (default ASCII).
        /// Конвертирует строку в байтовый массив. C кодировкой, передайте строкой тип кодировки (по умолчанию ASCII).
        /// ASCII, Unicode, UTF32, UTF7, UTF8.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytesEncoded(this string source, string encoding)
        {
            byte[] bytes;
            switch (encoding)
            {
                case "ASCII":
                    bytes = Encoding.ASCII.GetBytes(source);
                    break;

                case "Unicode":
                    bytes = Encoding.Unicode.GetBytes(source);
                    break;

                case "UTF32":
                    bytes = Encoding.UTF32.GetBytes(source);
                    break;

                case "UTF7":
                    bytes = Encoding.UTF7.GetBytes(source);
                    break;

                case "UTF8":
                    bytes = Encoding.UTF8.GetBytes(source);
                    break;

                default:
                    bytes = Encoding.ASCII.GetBytes(source);
                    break;
            }
            return bytes;
        }


        /// <summary>
        /// Convert Byte Array to string with encoding, pass Encoding type by string (default ASCII).
        /// Конвертирует байтовый массив в строку. C кодировкой, передайте строкой тип кодировки (по умолчанию ASCII).
        /// ASCII, Unicode, UTF32, UTF7, UTF8.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetStringEncoded(this byte[] bytes, string encoding)
        {
            string result;

            switch (encoding)
            {
                case "ASCII":
                    result = Encoding.ASCII.GetString(bytes);
                    break;

                case "Unicode":
                    result = Encoding.Unicode.GetString(bytes);
                    break;

                case "UTF32":
                    result = Encoding.UTF32.GetString(bytes);
                    break;

                case "UTF7":
                    result = Encoding.UTF7.GetString(bytes);
                    break;

                case "UTF8":
                    result = Encoding.UTF8.GetString(bytes);
                    break;

                default:
                    result = Encoding.ASCII.GetString(bytes);
                    break;
            }
            return result;
        }       
        
        //TODO: написать метод перевода в разные кодировки

        #region LINQ
        //Select: определяет проекцию выбранных значений

        //Where: определяет фильтр выборки

        //OrderBy: упорядочивает элементы по возрастанию

        //OrderByDescending: упорядочивает элементы по убыванию

        //ThenBy: задает дополнительные критерии для упорядочивания элементов возрастанию

        //ThenByDescending: задает дополнительные критерии для упорядочивания элементов по убыванию

        //Join: соединяет две коллекции по определенному признаку

        //GroupBy: группирует элементы по ключу

        //ToLookup: группирует элементы по ключу, при этом все элементы добавляются в словарь

        //GroupJoin: выполняет одновременно соединение коллекций и группировку элементов по ключу

        //Reverse: располагает элементы в обратном порядке

        //All: определяет, все ли элементы коллекции удовлятворяют определенному условию

        //Any: определяет, удовлетворяет хотя бы один элемент коллекции определенному условию

        //Contains: определяет, содержит ли коллекция определенный элемент

        //Distinct: удаляет дублирующиеся элементы из коллекции

        //Except: возвращает разность двух коллекцию, то есть те элементы, которые содератся только в одной коллекции

        //Union: объединяет две однородные коллекции

        //Intersect: возвращает пересечение двух коллекций, то есть те элементы, которые встречаются в обоих коллекциях

        //Count: подсчитывает количество элементов коллекции, которые удовлетворяют определенному условию

        //Sum: подсчитывает сумму числовых значений в коллекции

        //Average: подсчитывает cреднее значение числовых значений в коллекции

        //Min: находит минимальное значение

        //Max: находит максимальное значение

        //Take: выбирает определенное количество элементов

        //Skip: пропускает определенное количество элементов

        //TakeWhile: возвращает цепочку элементов последовательности, до тех пор, пока условие истинно

        //SkipWhile: пропускает элементы в последовательности, пока они удовлетворяют заданному условию, и затем возвращает оставшиеся элементы

        //Concat: объединяет две коллекции

        //Zip: объединяет две коллекции в соответствии с определенным условием

        //First: выбирает первый элемент коллекции

        //FirstOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию

        //Single: выбирает единственный элемент коллекции, если коллекция содердит больше или меньше одного элемента, то генерируется исключение

        //SingleOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию

        //ElementAt: выбирает элемент последовательности по определенному индексу

        //ElementAtOrDefault: выбирает элемент коллекции по определенному индексу или возвращает значение по умолчанию, если индекс вне допустимого диапазона

        //Last: выбирает последний элемент коллекции

        //LastOrDefault: выбирает последний элемент коллекции или возвращает значение по умолчанию
        #endregion

    }
}
