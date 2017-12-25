using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            A ab = new A();
            Console.ReadKey();
        }
    }

    partial class A
    {
        public A()
        {
            Console.WriteLine("A created");
        }
    }
    partial class A
    {
        static A()
        {
            Console.WriteLine("A created");
        }
    }
}
