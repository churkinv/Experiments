using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EnumExtension
    {
        /// <summary>
        /// Return name of enum as a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// Method will return Description attribute if exist on enum or name of enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.GetName());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute),
                false).FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute == null
                ? value.GetName() 
                : descriptionAttribute.Description;
        }
    }
}
