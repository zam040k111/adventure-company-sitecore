using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Foundation.Common.ValidationServices
{
    public static class Throw
    {
        public static void IfNull(object obj, string objName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(objName);
            }
        }

        public static void IfCondition(bool condition, string objName, string message)
        {
            if (condition)
            {
                throw new ArgumentException(message, objName);
            }
        }
        
        public static void IfEmpty<T>(IEnumerable<T> list, string objName)
        {
	        if (list == null || !list.Any())
	        {
		        throw new ArgumentException(objName);
	        }
        }

        public static void IfNullOrEmptyString(string str, string strName)
        {
	        if (string.IsNullOrWhiteSpace(str))
	        {
		        throw new ArgumentException(strName);
	        }
        }
    }
}