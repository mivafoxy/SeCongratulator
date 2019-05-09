using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AddUtil.ValidationRules
{
    public class LongerThan3ValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var data = (string)value;
            var result = new ValidationResult(data.Length > 3, "Пример валидации");
            return result;
        }
    }
}
