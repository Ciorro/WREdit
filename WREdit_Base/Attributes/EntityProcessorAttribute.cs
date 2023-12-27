namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EntityProcessorAttribute : Attribute
    {
        public string? DisplayName { get; set; }
    }
}
