namespace WREdit.Base.Entities.Properties
{
    public readonly struct ValueFormat
    {
        const char FormatSeparator = ':';

        public readonly string Type;
        public readonly string? Name;

        public ValueFormat(string format)
        {
            string[] parts = format.Split(FormatSeparator);

            if (parts.Length < 1 || parts.Length > 2)
            {
                throw new FormatException($"Invalid value format: {format}.");
            }

            Type = parts.ElementAtOrDefault(0)!;
            Name = parts.ElementAtOrDefault(1);
        }
    }
}
