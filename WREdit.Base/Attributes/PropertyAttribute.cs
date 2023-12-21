namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyAttribute : Attribute
    {
        public string? DisplayName { get; set; }

        public PropertyAttribute(string? displayName = null)
        {
            DisplayName = displayName;
        }
    }
}
