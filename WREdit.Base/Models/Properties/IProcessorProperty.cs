namespace WREdit.Base.Models.Properties
{
    public interface IProcessorProperty
    {
        string Name { get; }
        object Value { get; set; }
    }
}
