using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using RaccoonsLibraryCommon;

namespace I.O.andFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] ar = new byte[4096];

            // Uri uri = new Uri("file:///C:/Users/Raccoon/Desktop/What%20to%20do.txt"); // just example of correct URI scheme for file
            FileStream fs = new FileStream(@"C:\Users\Raccoon\Desktop\What to do.txt", FileMode.OpenOrCreate);// C:\Users\Raccoon\Desktop\What to do.txt
            fs.Read(ar, 0, ar.Length);
            string text = "";
            char[] ch = new char[ar.Length / sizeof(char)];
            Buffer.BlockCopy(ar, 0, ch, 0, 4096);
            Uri uri = new Uri("https://finance.ua/");
            byte[] html =  new WebClient().DownloadData(uri);
            byte[] file = new byte[4096];
            Buffer.BlockCopy(html, 0, file, 0,file.Length);


            FileStream fss = new FileStream(@"C:\Users\Raccoon\Desktop\File.txt", FileMode.CreateNew);
            fss.Write(file, 0,file.Length);
            text = Encoding.Unicode.GetString(ar);

            //text = StringHandler.GetStringEncoded(ar, "Unicode");

            //С помощью функции Split мы можем разделить строку на массив подстрок. 
            //В качестве параметра функция Split принимает массив символов или строк, которые и будут служить разделителями. 
            //Например, подсчитаем количество слов в сроке, разделив ее по пробельным символам:
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int count = 0;

            Regex regex = new Regex(@" \w*огд\w*"); // паттерн: слова в которых есть "неуд", * мы не знаем кол-во, может быть любое
            MatchCollection matches = regex.Matches(text);

            foreach (string s in words)
            {
                count++;
            }

            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }

            Console.WriteLine(count);
            Console.WriteLine(text);

            //foreach (int a in ch)
            //{
                 
            //    Console.Write(a);
            //}

            Console.Read();
        }
    }
}
