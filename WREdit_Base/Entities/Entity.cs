using System.Text.RegularExpressions;
using WREdit.Base.Entities.Properties;
using WREdit.Base.Extensions;

namespace WREdit.Base.Entities
{
    public class Entity : IEntity
    {
        public string FileName { get; }
        public string Content { get; private set; }
        public Range Selection { get; private set; }

        private Entity(string content, string fileName)
        {
            Content = content;
            FileName = fileName;
        }

        public static Entity FromFile(string fileName)
        {
            return new Entity(File.ReadAllText(fileName), fileName);
        }

        public void Prepend(string property)
        {
            int index = Selection.Start.GetOffset(Content.Length);
            Insert(index, property);
        }

        public void Append(string property)
        {
            int index = Selection.End.GetOffset(Content.Length);
            Insert(index, property);
        }

        public void RemoveSelection()
        {
            if (Selection.Equals(default))
            {
                return;
            }

            (int start, int length) = Selection.GetOffsetAndLength(Content.Length);

            Content = Content.Remove(start, length);
            Selection = new Range(start, start);
        }

        public IProperty? SelectNextProperty(PropertyFormat format)
        {
            var regex = new Regex(format.ToRegexString());
            var match = FindClosestMatch(regex.Matches(Content));

            if (match is null)
            {
                return null;
            }

            var property = new Property(format.PropertyName);

            for (int i = 0; i < format.ValueFormats.Count; i++)
            {
                var valueFormat = format.ValueFormats[i];

                Group group = match.Groups[i + 1];
                object value = valueFormat.Type switch
                {
                    "number" => group.GetValueAs<double>(),
                    _        => group.GetValueAs<string>()
                };

                property.AddValue(value, valueFormat.Name);
            }

            Selection = new Range(match.Index, match.Index + match.Length);
            return property;
        }

        private void Insert(int index, string property)
        {
            //Add new line to the property if it lacks it.
            if (!(property.EndsWith('\n') || property.EndsWith("\r\n")))
            {
                property = property + '\n';
            }

            Content = Content.Insert(index, property);

            index = index + property.Length;
            Selection = new Range(index, index);
        }

        private Match? FindClosestMatch(MatchCollection matches)
        {
            foreach (Match match in matches)
            {
                if (match.Index >= Selection.End.GetOffset(Content.Length))
                {
                    return match;
                }
            }

            return matches.FirstOrDefault();
        }
    }
}
