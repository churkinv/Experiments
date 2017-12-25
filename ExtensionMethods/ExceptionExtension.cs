using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Provide full messages of exceptions in friendly style. 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FullMessage(this Exception ex)
        {
            var builder = new StringBuilder();
            while (ex != null)
            {
                builder.AppendFormat($"{ex.Message}{Environment.NewLine}");
                ex = ex.InnerException;
            }

            return builder.ToString();
        }
    }
}
