using WREdit.Base.Attributes;
using WREdit.Base.Properties;

namespace WREdit.Common.Properties
{
    [PropertyTemplate("Properties/BooleanPropertyTemplate.xaml")]
    [TargetType(typeof(bool))]
    public class BooleanProperty : ProcessorProperty
    {
        public BooleanProperty(string propertyName, object instance)
            : base(propertyName, instance)
        { }

        public override object? Value
        {
            set
            {
                try
                {
                    base.Value = Convert.ToBoolean(value);
                }
                catch
                {
                    base.Value = false;
                }
            }
        }
    }
}
