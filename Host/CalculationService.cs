using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationService" in both code and config file together.
    public class CalculationService : ICalculationService
    {
        public int Calculate(int a, int b)
        {
            int sum = 0;
            sum = a + b;
            return sum;
        }
        
    }
}
