using WREdit.Base.Entities.Properties;

namespace WREdit.Base.Entities
{
    public interface IEntity
    {
        string Source { get; }
        Range Selection { get; }

        void RemoveSelection();
        void Prepend(string property);
        void Append(string property);

        IProperty? SelectNextProperty(PropertyFormat format);
    }
}
