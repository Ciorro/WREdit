namespace WREdit.Base.Properties
{
    public interface IProcessorProperty
    {
        string Name { get; init; }
        object? Value { get; set; }
    }
}
