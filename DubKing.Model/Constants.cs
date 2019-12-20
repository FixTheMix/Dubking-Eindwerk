using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class Constants
    {
        public class ErrorMessages
        {
            public const string LessThen25 = "Must be less then 25 characters.";
            public const string Mandatory = "Mandatory.";
            public const string Exists = "Allready Exists.";
            public const string LessThen50 = "Must be less then 50 characters.";
            public const string LessThen1k = "Must be less then 1000 characters.";
            public const string InValidEMail = "Not a valid mailaddress"; 
            public static string LessthenVariable(int count)
            {
                return $"Must be less the {count} characters.";
            }
            public static string MinimumValue(int min)
            {
                return $"Value must be > {min}.";
            }
        }
    }
}
