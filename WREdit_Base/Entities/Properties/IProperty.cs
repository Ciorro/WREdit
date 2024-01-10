namespace WREdit.Base.Entities.Properties
{
    public interface IProperty
    {
        string Name { get; }
        int ValueCount { get; }

        object this[int index] { get; }
        object this[string name] { get; }

        T GetValue<T>(int index);
        T GetValue<T>(string name);
    }
}
