using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Program
    {
        volatile string myString = "ItIsMyString";
        
        static void Main(string[] args)
        {
            Console.WriteLine("First entry");

            Thread.Sleep(5000);

            Console.WriteLine("Second entry");
            Print();

            Console.ReadKey();

        }

        async static Task<string> GetStringASync()
        {
           var result = await Task.Run(() => 
           {
               Thread.Sleep(2000);
               return "Yo ho ho";
           });

            return result;
        }

        async static void Print()
        {
            Console.WriteLine(await GetStringASync());
        }
    }

    class ThreadTest
    {


    }
}
