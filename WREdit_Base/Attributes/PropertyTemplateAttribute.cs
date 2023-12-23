namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PropertyTemplateAttribute : Attribute
    {
        public string PropertyTemplatePath { get; }

        public PropertyTemplateAttribute(string path)
        {
            PropertyTemplatePath = path;
        }
    }
}
