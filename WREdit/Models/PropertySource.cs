namespace WREdit.Models
{
    internal class PropertySource
    {
        public string PropertyName { get; }

        public PropertySource(string propertyName)
        {
            PropertyName = propertyName;
        }

        public TVal GetValueAt<TVal>(int valueIndex)
        {
            throw new NotImplementedException();
        }

        public void SetValueAt<TVal>(int valueIndex, TVal value)
        {

        }
    }
}
