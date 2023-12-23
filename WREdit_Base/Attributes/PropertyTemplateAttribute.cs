namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PropertyTemplateAttribute : Attribute
    {
        public string? Assembly { get; set; }
        public string? TemplateKey { get; set; }
        public string PropertyTemplatePath { get; }

        public PropertyTemplateAttribute(string path)
        {
            PropertyTemplatePath = path;
        }
    }
}
