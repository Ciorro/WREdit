using System.Globalization;
using WREdit.Base.Attributes;
using WREdit.Base.Processing.Properties;

namespace WREdit.Common.Properties
{
    [PropertyTemplate("Properties/NumberPropertyTemplate.xaml")]
    [TargetType(typeof(byte))]
    [TargetType(typeof(sbyte))]
    [TargetType(typeof(short))]
    [TargetType(typeof(ushort))]
    [TargetType(typeof(int))]
    [TargetType(typeof(uint))]
    [TargetType(typeof(long))]
    [TargetType(typeof(ulong))]
    [TargetType(typeof(float))]
    [TargetType(typeof(double))]
    public class NumberProperty : ProcessorProperty
    {
        public NumberProperty(string property, object instance)
            : base(property, instance)
        { }

        public override object? Value
        {
            get => Convert.ToString(base.Value, CultureInfo.CurrentCulture);
            set
            {
                try
                {
                    var targetType = Nullable.GetUnderlyingType(Property.PropertyType) ?? Property.PropertyType;

                    if (targetType == typeof(byte))
                        base.Value = Convert.ToByte(value);
                    if (targetType == typeof(sbyte))
                        base.Value = Convert.ToSByte(value);

                    if (targetType == typeof(short))
                        base.Value = Convert.ToInt16(value);
                    if (targetType == typeof(ushort))
                        base.Value = Convert.ToUInt16(value);

                    if (targetType == typeof(int))
                        base.Value = Convert.ToInt32(value);
                    if (targetType == typeof(uint))
                        base.Value = Convert.ToUInt32(value);

                    if (targetType == typeof(long))
                        base.Value = Convert.ToInt64(value);
                    if (targetType == typeof(ulong))
                        base.Value = Convert.ToUInt64(value);

                    if (targetType == typeof(float))
                        base.Value = Convert.ToSingle(value);
                    if (targetType == typeof(double))
                        base.Value = Convert.ToDouble(value);
                }
                catch
                {
                    base.Value = default;
                }
            }
        }
    }
}
