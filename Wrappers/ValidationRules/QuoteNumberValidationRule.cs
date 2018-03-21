using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UITestGround.Wrappers.ValidationRules
{
    public class QuoteNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Description = value.ToString();
            if (string.IsNullOrWhiteSpace(Description))
            {
                return new ValidationResult(false, "Quote Number cannot be empty");
            }
            return ValidationResult.ValidResult;
        }
    }
}
