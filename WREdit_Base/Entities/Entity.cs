using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using WREdit.Base.Entities.Properties;
using WREdit.Base.Extensions;

namespace WREdit.Base.Entities
{
    public class Entity : IEntity
    {
        public string FileName { get; private set; }
        public string Content { get; private set; }
        public Range Selection { get; private set; }

        public Entity(string fileName)
        {
            FileName = fileName;
            Content = "";
        }

        public void Load()
        {
            Content = File.ReadAllText(FileName);

            if (!(TrySelectNextProperty("$NAME_STR string", out _) || 
                  TrySelectNextProperty("$NAME number", out _)))
            {
                throw new InvalidDataException("Invalid script.");
            }
        }

        public void Save()
        {
            File.WriteAllText(FileName, Content);
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

        public IProperty? SelectNextProperty(PropertyFormat format, bool peek = false)
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

            if (!peek)
            {
                Selection = new Range(match.Index, match.Index + match.Length);
            }

            return property;
        }

        public bool TrySelectNextProperty(PropertyFormat format, [MaybeNullWhen(false)] out IProperty property)
        {
            return (property = SelectNextProperty(format)) is not null;
        }

        private void Insert(int index, string property)
        {
            if (!(property.EndsWith('\n') || property.EndsWith("\r\n")))
            {
                property = property + '\n';
            }

            if (!(property.StartsWith('\n') || property.StartsWith("\r\n")))
            {
                property = '\n' + property;
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
