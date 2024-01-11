using System.Globalization;
using System.Text.RegularExpressions;

namespace WREdit.Base.Extensions
{
    internal static class RegexGroupExtensions
    {
        public static T GetValueAs<T>(this Group group) where T : IParsable<T>
        {
            return (T)T.Parse(group.Value, CultureInfo.InvariantCulture);
        }
    }
}
