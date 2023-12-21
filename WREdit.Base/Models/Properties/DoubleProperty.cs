namespace WREdit.Base.Models.Properties
{
    internal class DoubleProperty : IProcessorProperty
    {
        public string Name { get; }

        public DoubleProperty(string name, int defaultValue = 0)
        {
            Name = name;
            Value = defaultValue;
        }

        //TODO: Convert?
        public object Value { get; set; }
    }
}
