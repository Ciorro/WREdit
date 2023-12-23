namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ProcessorAttribute : Attribute
    {
        public string? DisplayName { get; set; }
    }
}
