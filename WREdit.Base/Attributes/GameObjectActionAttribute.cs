namespace WREdit.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GameObjectActionAttribute : Attribute
    {
        public string? DisplayName { get; set; }
    }
}
