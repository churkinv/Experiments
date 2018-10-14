using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLvsThreads
{
    class Program
    {
        static void Main(string[] args) // here we have two threads main and GC
        {        

            // Get cores of CPU

            // Thread 1 

            // Thread 2

            // Thread 3 

            try
            {

                // Task 1 
                
                // Task 2 

                // Task 3

                // Error Task

                Task e_task = Task.Factory.StartNew(() =>
                {
                    int zero = 0;
                    int number = 1000;// zero;
                    Console.WriteLine(number);
                  
                });
               // Task.WaitAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
           
            }
            Console.ReadKey();
        }
    }


    class A
    {
        public A()
        {

        }
    }
}
