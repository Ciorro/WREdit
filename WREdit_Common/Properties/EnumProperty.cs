using WREdit.Base.Attributes;
using WREdit.Base.Processing.Properties;

namespace WREdit.Common.Properties
{
    [PropertyTemplate("Properties/EnumPropertyTemplate.xaml")]
    [TargetType(typeof(Enum))]
    public class EnumProperty : ProcessorProperty
    {
        public EnumProperty(string propertyName, object instance)
            : base(propertyName, instance)
        { }

        public System.Collections.IEnumerable? PossibleValues
        {
            get
            {
                if (Property.PropertyType.IsAssignableTo(typeof(Enum)))
                {
                    return Enum.GetValues(Property.PropertyType);
                }

                return null;
            }
        }
    }
}
