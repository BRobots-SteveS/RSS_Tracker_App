using System.Globalization;

namespace Rss_Mobile_App.Converters
{
    public class NewFeedTitleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Guid)) return "New feed";
            if (parameter == null || parameter.GetType() != typeof(string)) return "New feed";
            if (!string.IsNullOrEmpty((string)parameter)) return (string)parameter;
            return "New feed";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
