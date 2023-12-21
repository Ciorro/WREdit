using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WREdit.Converters
{
    internal class TypeToInstanceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new TypeToInstanceConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetType()!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type type)
            {
                return Activator.CreateInstance(type)!;
            }

            throw new ArgumentException("Invalid type.", nameof(value));
        }
    }
}
