using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public static class LambdaExpressionExtensions
    {
        /// <summary>
        /// TODO: to investigate reflection!
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member as PropertyInfo;
        }
    }
}
