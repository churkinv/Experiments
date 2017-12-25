using System;
using System.Collections.Generic;

namespace RacconsLibraryCommon
{
    public class LoggingService 
    {
        public static void WriteToFile(List<ILoggable> changedItems)
        {
            foreach (var item in changedItems)
            {
                //TODO refactor to write log into file
                Console.WriteLine(item.Log());
            }
        }       
    }
}
