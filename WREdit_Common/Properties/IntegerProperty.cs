using WREdit.Base.Attributes;
using WREdit.Base.Properties;

namespace WREdit.Common.Properties
{
    [PropertyTemplate("Properties/IntegerPropertyTemplate.xaml")]
    [TargetType(typeof(int))]
    public class IntegerProperty : ProcessorProperty
    {
        public IntegerProperty(string property, object instance)
            : base(property, instance)
        { }

        public IntegerProperty(string property, object instance, int defaultValue = 0)
            : base(property, instance)
        {
            Value = defaultValue;
        }

        public override object? Value
        {
            set
            {
                try
                {
                    base.Value = Convert.ToInt32(value);
                }
                catch (FormatException)
                {
                    base.Value = default;
                }
            }
        }
    }
}
