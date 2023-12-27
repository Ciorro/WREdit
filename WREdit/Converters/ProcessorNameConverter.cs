using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;

namespace WREdit.Converters
{
    internal class ProcessorNameConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ProcessorNameConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type processorType)
            {
                if (processorType.TryGetCustomAttribute<EntityProcessorAttribute>(out var attribute))
                {
                    return attribute.DisplayName ?? processorType.Name;
                }

                return processorType.Name;
            }

            throw new ArgumentException($"{value?.GetType().Name} is not a processor.", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
