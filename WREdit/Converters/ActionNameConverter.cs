using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;
using WREdit.Base.Actions;
using WREdit.Base.Attributes;

namespace WREdit.Converters
{
    internal class ActionNameConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ActionNameConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IGameObjectAction action)
            {
                var type = action.GetType();
                var attr = type.GetCustomAttribute<GameObjectActionAttribute>();

                return attr?.DisplayName ?? type.Name;
            }

            throw new ArgumentException("Value is not an action", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
