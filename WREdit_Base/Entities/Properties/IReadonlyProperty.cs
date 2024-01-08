namespace WREdit.Base.Entities.Properties
{
    public interface IReadonlyProperty
    {
        string Name { get; }

        object this[int index] { get; }
        object this[string name] { get; }

        T GetValue<T>(int index);
        T GetValue<T>(string name);
    }
}
