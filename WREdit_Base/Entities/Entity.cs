using System.Globalization;
using System.Text;
using WREdit.Base.Entities.Properties;

namespace WREdit.Base.Entities
{
    public class Entity
    {
        private readonly string _source;

        private int _caretPosition = 0;
        private int _selectionStart = 0;
        private int _selectionEnd = 0;

        public Entity(string entitySource)
        {
            _source = entitySource;
        }

        public IReadonlyProperty? SelectProperty(string propertyName, params string[] values)
        {
            int propertyIndex = _source.IndexOf(propertyName, _caretPosition);
            if (propertyIndex == -1)
            {
                return null;
            }

            var property = new Property(ReadWord());
            _selectionStart = _caretPosition;

            foreach (var valueDef in values)
            {
                string[] valueInfo = valueDef.Split(':');

                if (valueInfo.Length < 1 || valueInfo.Length > 2)
                    throw new Exception($"Invalid value definition: {valueDef}.");

                string valueType = valueInfo.ElementAtOrDefault(0)!;
                string? valueName = valueInfo.ElementAtOrDefault(1);

                object value = valueType switch
                {
                    "string" => ReadString(),
                    "number" => ReadNumber(),
                    _ => ReadWord()
                };

                property.AddValue(value, valueName);
            }

            _selectionEnd = _caretPosition;
            return property;
        }

        private double ReadNumber()
        {
            if (!double.TryParse(ReadWord(), CultureInfo.InvariantCulture, out var number))
            {
                throw new Exception("Failed to read a number.");
            }

            return number;
        }

        private string ReadString()
        {
            try
            {
                var sb = new StringBuilder();

                while (_source[_caretPosition] != '"')
                {
                    if (!char.IsWhiteSpace(_source[_caretPosition]))
                    {
                        throw new Exception("Failed to read a string");
                    }
                    _caretPosition++;
                }

                while (_source[_caretPosition] != '"')
                {
                    sb.Append(_source[_caretPosition++]);
                }

                //Skip closing quote
                _caretPosition++;

                return sb.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Failed to read a string.");
            }
        }

        private string ReadWord()
        {
            try
            {
                var sb = new StringBuilder();

                //Skip spaces before word
                while (_source[_caretPosition] == ' ')
                    _caretPosition++;

                //Read word
                while (_source[_caretPosition] != ' ')
                {
                    sb.Append(_source[_caretPosition++]);
                }

                return sb.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Failed to read a word.");
            }
        }
    }
}
