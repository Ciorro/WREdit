namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class GameObjectActionPropertyAttribute : Attribute
    {
        public Type PropertyType { get; }

        public GameObjectActionPropertyAttribute(Type propertyType, string? displayName = null)
        {
            PropertyType = propertyType;
            _displayName = displayName;
        }

        private string? _displayName;
        public string DisplayName
        {
            get => _displayName ?? PropertyType.Name;
            set => _displayName = value;
        }
    }
}
