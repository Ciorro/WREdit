namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class GameObjectActionPropertyAttribute : Attribute
    {
        public string? DisplayName { get; set; }

        public GameObjectActionPropertyAttribute(string? displayName = null)
        {
            DisplayName = displayName;
        }
    }
}
