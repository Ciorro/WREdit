namespace WREdit.Base.Processing.Properties
{
    public interface IProcessorProperty
    {
        string Name { get; init; }
        object? Value { get; set; }
    }
}
