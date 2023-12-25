namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TargetTypeAttribute : Attribute
    {
        public Type TargetType { get; }

        public TargetTypeAttribute(Type type)
        {
            TargetType = type;
        }
    }
}
