using System.Windows;

namespace WREdit.Extensions
{
    internal static class ResourceDictionaryExtensions
    {
        public static object? FirstOrDefault(this ResourceDictionary resourceDictionary)
        {
            var enumerator = resourceDictionary.Values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                return enumerator.Current as DataTemplate;
            }

            return null;
        }
    }
}
