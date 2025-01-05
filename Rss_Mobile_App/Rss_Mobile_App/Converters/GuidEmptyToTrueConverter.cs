using System.Globalization;

namespace Rss_Mobile_App.Converters
{
    public class GuidEmptyToTrueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Guid)) return value;
            var id = (Guid)value;
            return id == Guid.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
