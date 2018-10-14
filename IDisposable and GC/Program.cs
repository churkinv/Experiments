using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IDisposable_and_GC
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }


    #region *BEST PRACTICES*
    // Is a must
    // 1) Dispose of IDisposable object as soon as you can
    // 2) If you use IDisposable object as instance fields, implement IDisposable
    // You should have
    // 3) Allow Dispose() to be called multiple times and don`t throw exceptions
    // 4) Implement IDisposable to support disposing resources in a class hierarchy
    // 5) If you use unmanaged resources, declare a finalizer which ckeans them up
    // You could have
    // 6) Enable Code Analysis with CA2000 enabled - but don`t rely on it
    // 7) Better to: if you implement an interface and use IDisposable fields, extend your interface from IDisposable
    // 8) If you implement IDisposable, don`t implement it explicitly
    #endregion

    class MyClass : MyBaseClass
    {

        private StreamReader _fileReader;
        private IntPtr _unmanagedPointer;


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this); // we are telling GC don`t collect it as we handle it and save time and resources as result
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _fileReader != null) // && _fileSystemWathcer!= null // or another instance
            {
                _fileReader.Dispose();
                _fileReader = null;
            }

            if (_unmanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }

            // and as variant we can call 
            base.Dispose(disposing); // in case we inhereted (?)
        }

        //GC can`t free unmanaged resources, like our _unmanagedPointer, so we need to use  Finilizer
        ~MyClass()
        {
            Dispose(false);
        }

    }


    /// <summary>
    /// Not finished
    /// </summary>
    internal class MyBaseClass : IDisposable
    {
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
