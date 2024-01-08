namespace WREdit.Base.Entities.Properties
{
    public class Property : IReadonlyProperty
    {
        private readonly List<(object, string)> _values = new();
        public string Name { get; }

        public Property(string name)
        {
            Name = name;
        }

        public object this[int index]
        {
            get => _values[index].Item1;
        }

        public object this[string name]
        {
            get => _values.First(v => v.Item2 == name).Item1;
        }

        public void AddValue(object value, string? name = null)
        {
            name ??= Random.Shared.Next().ToString();

            if (!_values.Any(v => v.Item2 == name))
            {
                _values.Add((value, name));
            }
        }

        public T GetValue<T>(int index)
        {
            return (T)this[index];
        }

        public T GetValue<T>(string name)
        {
            return (T)this[name];
        }
    }
}
