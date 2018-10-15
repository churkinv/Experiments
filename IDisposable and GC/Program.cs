using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    // 5) If you use unmanaged resources, declare a finalizer which cleans them up
    // You could have
    // 6) Enable Code Analysis with CA2000 enabled - but don`t rely on it
    // 7) Better to: if you implement an interface and use IDisposable fields, extend your interface from IDisposable
    // 8) If you implement IDisposable, don`t implement it explicitly
    // and read this: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/dispose-pattern?view=netframework-4.7.2
    #endregion

    class MyClass
    {
        private SqlCommand _sqlCommand;
        private IntPtr _unmanagedPointer;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this); // we are telling GC don`t collect it as we handle it and save time and resources as result, and Finilizer wont be called as well
        }

        protected virtual void Dispose(bool disposing)  // we have to dispose both managed and unmanaged resources
        {
            if (disposing && _sqlCommand != null) // && _fileSystemWathcer!= null // or another instance
            {
                _sqlCommand.Dispose();
                _sqlCommand = null;
            }

            if (_unmanagedPointer != IntPtr.Zero) // we need to clear ONLY unmanaged resource, as we don`t know the state of managed resource at the time of finilization
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }      
        }

        // GC can`t free unmanaged resources, like our _unmanagedPointer, so we need to use  Finalizer
        // in 99% we don`t need it as in our case we use it onlu to free _unmanagedPointer better to separate logic with managed and unmanaged resources
        ~MyClass()
        {
            Dispose(false); // in this case we cler only unmannaged resources
        }
    }
}
