using WREdit.Base.Attributes;
using WREdit.Base.Properties;

namespace WREdit.Common.Properties
{
    [PropertyTemplate("Properties/StringPropertyTemplate.xaml")]
    [TargetType(typeof(string))]
    public class StringProperty : ProcessorProperty
    {
        public StringProperty(string propertyName, object instance)
            : base(propertyName, instance)
        { }

        public override object? Value
        {
            set => base.Value = value?.ToString();
        }
    }
}
