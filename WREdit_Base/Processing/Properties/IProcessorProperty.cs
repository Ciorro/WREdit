namespace WREdit.Base.Processing.Properties
{
    public interface IProcessorProperty
    {
        string Name { get; }
        string? Help { get; }
        object? Value { get; set; }
    }
}
