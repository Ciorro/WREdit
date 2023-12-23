using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WREdit.Base.Attributes;
using WREdit.Base.Extensions;
using WREdit.Extensions;

namespace WREdit.Templating
{
    internal class PropertyTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            return GetPropertyTemplate(item.GetType());
        }

        private DataTemplate? GetPropertyTemplate(Type processorPropertyType)
        {
            string assembly = processorPropertyType.Assembly.GetName().Name!;
            object? template = null;

            if (processorPropertyType.TryGetCustomAttribute<PropertyTemplateAttribute>(out var attribute))
            {
                string assemblyName = attribute.Assembly ?? assembly;
                string templatePath = attribute.PropertyTemplatePath;
                string resourcePath = $"pack://application:,,,/{assemblyName};component/{templatePath}";

                try
                {
                    var templateResource = new ResourceDictionary();
                    templateResource.Source = new Uri(resourcePath);

                    if (!string.IsNullOrEmpty(attribute.TemplateKey))
                    {
                        template = templateResource[attribute.TemplateKey];
                    }
                    else
                    {
                        template = templateResource.FirstOrDefault();
                    }
                }
                catch { }
            }

            return template as DataTemplate;
        }
    }
}
