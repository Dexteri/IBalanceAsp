using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IBalance.Domain.Attributes
{
    public class PhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                Regex phoneRegex = new Regex(@"\+7\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}");
                if (phoneRegex.IsMatch(value.ToString()))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
