using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Чтобы управлять типом и получать всю информацию о нем, нам надо сперва получить данный тип. 
            //Это можно сделать тремя способами: 
            //с помощью ключевого слова typeof:
            Type type = typeof(User);
            Console.WriteLine(type);
            Console.WriteLine(type.ToString());
            //Console.ReadKey();

            //с помощью метода GetType() класса Object:
            User user = new User("Tom", 30);
            Type myType = user.GetType();

            Console.WriteLine(myType.ToString());
            //Console.ReadKey();
            //и применяя статический метод Type.GetType().
            Type myType2 = Type.GetType("Reflection.User", false, true);
            //Второй параметр указывает, будет ли генерироваться исключение, если класс не удастся найти. 
            //В данном случае значение false означает, что исключение не будет генерироваться. 
            //И третий параметр указывает, надо ли учитывать регистр символов в первом параметре. 
            //Значение true означает, что регистр не учитывается.
            Console.WriteLine(myType2.ToString());
            Console.ReadKey();
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public User(string n, int a)
        {
            Name = n;
            Age = a;
        }
        public void Display()
        {
            Console.WriteLine("Имя: {0}  Возраст: {1}", this.Name, this.Age);
        }
        public int Payment(int hours, int perhour)
        {
            return hours * perhour;
        }
    }

    //********************************************
    //   Основной функционал рефлексии сосредоточен в пространстве имен System.Reflection. 
    //   В нем мы можем выделить следующие основные классы:
    //Assembly: класс, представляющий сборку и позволяющий манипулировать этой сборкой
    //AssemblyName: класс, хранящий информацию о сборке
    //MemberInfo: базовый абстрактный класс, определяющий общий функционал для классов EventInfo, FieldInfo, MethodInfo и PropertyInfo
    //EventInfo: класс, хранящий информацию о событии
    //FieldInfo: хранит информацию об определенном поле типа
    //MethodInfo: хранит информацию об определенном методе
    //PropertyInfo: хранит информацию о свойстве
    //ConstructorInfo: класс, представляющий конструктор
    //Module: класс, позволяющий получить доступ к определенному модулю внутри сборки
    //ParameterInfo: класс, хранящий информацию о параметре метода

    //********************************************
    //    Класс System.Type представляет изучаемый тип, инкапсулируя всю информацию о нем.
    //    С помощью его свойств и методов можно получить эту информацию.Некоторые из его свойств и методов:
    //Метод FindMembers() возвращает массив объектов MemberInfo данного типа
    //Метод GetConstructors() возвращает все конструкторы данного типа в виде набора объектов ConstructorInfo
    //Метод GetEvents() возвращает все события данного типа в виде массива объектов EventInfo
    //Метод GetFields() возвращает все поля данного типа в виде массива объектов FieldInfo
    //Метод GetInterfaces() получает все реализуемые данным типом интерфейсы в виде массива объектов Type
    //Метод GetMembers() возвращает все члены типа в виде массива объектов MemberInfo
    //Метод GetMethods() получает все методы типа в виде массива объектов MethodInfo
    //Метод GetProperties() получает все свойства в виде массива объектов PropertyInfo
    //Свойство IsAbstract возвращает true, если тип является абстрактным
    //Свойство IsArray возвращает true, если тип является массивом
    //Свойство IsClass возвращает true, если тип представляет класс
    //Свойство IsEnum возвращает true, если тип является перечислением
    //Свойство IsInterface возвращает true, если тип представляет интерфейс

}
