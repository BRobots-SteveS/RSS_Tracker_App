using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Mobile_App.Converters
{
    public class LabelMaxLengthConverter : IValueConverter
    {
        private const int MAXLENGTH = 50;
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(string)) return value;
            string labelText = (string)value;

            if (string.IsNullOrEmpty(labelText))
                return value;
            if (labelText.Length > MAXLENGTH)
                return $"{labelText[..MAXLENGTH]}...";
            else
                return labelText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
