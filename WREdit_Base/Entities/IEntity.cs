using System.Diagnostics.CodeAnalysis;
using WREdit.Base.Entities.Properties;

namespace WREdit.Base.Entities
{
    public interface IEntity
    {
        string FileName { get; }
        string Content { get; }
        Range Selection { get; }

        void Load();
        void Save();

        void RemoveSelection();
        void Prepend(string property);
        void Append(string property);

        IProperty? SelectNextProperty(PropertyFormat format, bool peek = false);
        bool TrySelectNextProperty(PropertyFormat format, [MaybeNullWhen(false)] out IProperty property);
    }
}
