using System.Text;

namespace WREdit.Base.Entities.Properties
{
    public class PropertyFormat
    {
        public string PropertyName { get; private set; }
        public IReadOnlyList<ValueFormat> ValueFormats { get; private set; }

        public PropertyFormat(string format)
        {
            ArgumentException.ThrowIfNullOrEmpty(format);

            try
            {
                string[] parts = format.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                if (!IsValidPropertyName(parts[0]))
                {
                    throw new FormatException($"Invalid property name.");
                }
                PropertyName = parts[0];

                var values = new List<ValueFormat>();
                foreach (var value in parts.Skip(1))
                {
                    values.Add(new ValueFormat(value));
                }
                ValueFormats = values;
            }
            catch (FormatException)
            {
                throw;
            }
            catch
            {
                throw new FormatException("Invalid property format.");
            }
        }

        public string ToRegexString()
        {
            var sb = new StringBuilder("(?<=\\s|^)");
            sb.Append($"\\{PropertyName}\\s+");

            sb.AppendJoin("\\s+", ValueFormats.Select((vf, i) =>
            {
                var valueName = vf.Name ?? $"Value{i}";

                var regex = vf.Type switch
                {
                    "number" => $"(?'{valueName}'-?\\d+(?>\\.\\d+)?)",
                    "string" => $"\"(?'{valueName}'.*?)\"",
                    _        => $"(?'{valueName}'[^\\s]+)"
                };

                return regex;
            }));

            return sb.ToString();
        }

        private bool IsValidPropertyName(string propertyName)
        {
            return propertyName.StartsWith('$') && propertyName.All(c => !char.IsWhiteSpace(c));
        }

        public static implicit operator PropertyFormat(string format)
        {
            return new PropertyFormat(format);
        }
    }
}
