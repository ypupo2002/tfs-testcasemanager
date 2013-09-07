using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp.BusinessLogic.Converters
{
    public class TestStepPropertiesConverter : IValueConverter
    {
        public TestStepPropertiesConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParameterizedString currentTestStepProperty = value as ParameterizedString;
            string propertyPlainText = currentTestStepProperty.ToPlainText();

            return propertyPlainText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
