using System.Globalization;
using System.Text;
using WREdit.Base.Entities.Properties;

namespace WREdit.Base.Entities
{
    public class Entity : IEntity
    {
        public string Source { get; private set; }
        public Range Selection { get; private set; }

        public Entity(string source)
        {
            Source = source;
        }

        public void Prepend(string property)
        {
            int index = Selection.Start.GetOffset(Source.Length);
            Insert(index, property);
        }

        public void Append(string property)
        {
            int index = Selection.End.GetOffset(Source.Length);
            Insert(index, property);
        }

        public void RemoveSelection()
        {
            if (Selection.Equals(default))
            {
                return;
            }

            (int start, int length) = Selection.GetOffsetAndLength(Source.Length);

            Source = Source.Remove(start, length);
            Selection = new Range(start, start);
        }

        public IProperty? SelectNextProperty(PropertyFormat format)
        {
            int startIndex = Selection.End.GetOffset(Source.Length);

            int propertyIndex = Source.IndexOf(format.PropertyName, startIndex);
            if (propertyIndex == -1)
            {
                propertyIndex = Source.IndexOf(format.PropertyName);
                if (propertyIndex == -1)
                {
                    return null;
                }
            }

            int caret = propertyIndex;

            var propertyName = ReadWordAs<string>(ref caret);
            var property = new Property(propertyName);

            foreach (var valueDef in format.ValueFormats)
            {
                object value = valueDef.Type switch
                {
                    "string" => ReadString(ref caret),
                    "number" => ReadWordAs<float>(ref caret),
                    _ => ReadWordAs<string>(ref caret)
                };

                property.AddValue(value, valueDef.Name);
            }

            Selection = new Range(propertyIndex, caret);
            return property;
        }

        private void Insert(int index, string property)
        {
            //Add new line to the property if it lacks it.
            if (!(property.EndsWith('\n') || property.EndsWith("\r\n")))
            {
                property = property + '\n';
            }

            Source = Source.Insert(index, property);

            index = index + property.Length;
            Selection = new Range(index, index);
        }

        private T ReadWordAs<T>(ref int caret) where T : IParsable<T>
        {
            try
            {
                var sb = new StringBuilder();

                //Skip spaces before word
                while (char.IsWhiteSpace(Source[caret]))
                    caret++;

                //Read word
                while (!char.IsWhiteSpace(Source[caret]))
                {
                    sb.Append(Source[caret++]);
                }

                return T.Parse(sb.ToString(), CultureInfo.InvariantCulture);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Failed to read a word.");
            }
        }

        private string ReadString(ref int caret)
        {
            try
            {
                var sb = new StringBuilder();

                while (Source[caret] != '"')
                {
                    if (!char.IsWhiteSpace(Source[caret]))
                    {
                        throw new Exception("Failed to read a string");
                    }
                    caret++;
                }

                //Skip opening quote
                caret++;

                while (Source[caret] != '"')
                {
                    sb.Append(Source[caret++]);
                }

                //Skip closing quote
                caret++;

                return sb.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Failed to read a string.");
            }
        }
    }
}
