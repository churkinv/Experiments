using System.Runtime;
using System;

// Приказываем компилятору проверять код
// на совместимость с CLS
[assembly: CLSCompliant(true)]
namespace RichterCLR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Object obj = new Object();
            //Console.WriteLine(obj.ToString());
            //Console.WriteLine(obj.GetType());
            ////int a = 5;
            ////int b = 6;
            //int c = 7;
            // Console.WriteLine(a+b*c);
            //object a = 1;
            //object b = 1;
            //if (a == b)
            //    Console.WriteLine("Hi");
            //Console.ReadLine();

            //Также при анализе производительности может пригодиться класс System.
            //Runtime.ProfileOptimization.Он заставляет CLR сохранить(в файле) инфор -        
            //мацию о том, какие методы проходят JIT - компиляцию во время выполнения при-
            //ложения.Если машина, на которой работает приложение, оснащена несколькими
            //процессорами, при будущих запусках приложения JIT-компилятор параллельно
            //компилирует эти методы в других программных потоках. В результате прило -
            //жение работает быстрее, потому что несколько методов компилируются парал -
            //лельно, причем это происходит во время инициализации приложения(вместо
            //JIT - компиляции).
            //ProfileOptimization profileOptimization;

            #region Chapter 4 Types
            // Приведение типа не требуется, т. к. new возвращает объект Employee,
            // а Object — это базовый тип для Employee.
            //Object o = new Employee();
            // Приведение типа обязательно, т. к. Employee — производный от Object
            // В других языках (таких как Visual Basic) компилятор не потребует
            // явного приведения
            //Employee e = (Employee)o;

            // test on type casting

            Object o1 = new Object();
            Object o2 = new B();
            Object o3 = new D();
            Object o4 = o3;

            B b1 = new B();
            B b2 = new D();
            D d1 = new D();

            //B b3 = new Object(); //CTE
            //D d2 = new Object(); //CTE

            B b4 = d1;

            //D d3 = b2;   //CTE

            D d4 = (D)d1;
            D d5 = (D)b2;

            //D d6 = (D)b1; // RTE
            //B b5 = (B)o1; // RTE

            B b6 = (D)b2; 

            Console.WriteLine(b6.ToString());
                                 
            #endregion

            #region Chapter 5 primitive type dynamic
            // Пример вызова статического метода Concat(String,
            // String) класса String:
            //dynamic stringType = new StaticMemberDynamicWrapper(typeof(String));
            //var r = stringType.Concat("A", "B"); // Динамический вызов статического
            //                                     // метода Concat класса String
            //Console.WriteLine(r); // выводится "AB"
            #endregion
            Console.ReadLine();
        }
    }

    public sealed class Hello
    {
        static int x = 5;
        static Hello()
        {
            x = 10;
        }
    }

    public struct Point
    {
        private Int32 a;
    }

    #region Chapter 4 Types
    // Этот тип неявно наследует от типа System.Object
    internal class Employee
    {

    }

    // Базовый класс
    internal class B
    {
    }
    // Производный класс
    internal class D : B
    {

    }
    #endregion

    #region Chapter 1
    // Предупреждения выводятся, потому что класс является открытым
    public sealed class SomeLibraryType
    {
        // Предупреждение: возвращаемый тип 'SomeLibrary.SomeLibraryType.Abc()'
        // не является CLS-совместимым
        public UInt32 Abc() { return 0; }

        // Предупреждение: идентификаторы 'SomeLibrary.SomeLibraryType.abc()',
        // отличающиеся только регистром символов, не являются
        // CLS-совместимыми
        public void abc() { }
        // Предупреждения нет: закрытый метод
        private UInt32 ABC() { return 0; }
    }
    #endregion
}
