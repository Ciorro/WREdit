namespace WREdit.Base.Models.Properties
{
    internal class IntegerProperty : IProcessorProperty
    {
        public string Name { get; }

        public IntegerProperty(string name, int defaultValue = 0)
        {
            Name = name;
            Value = defaultValue;
        }

        //TODO: Convert?
        public object Value { get; set; }
    }
}
