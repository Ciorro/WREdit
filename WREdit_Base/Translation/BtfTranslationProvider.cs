using WRLang;

namespace WREdit.Base.Translation
{
    public class BtfTranslationProvider : ITranslationProvider
    {
        private readonly Dictionary<int, string> _translations = new();

        public BtfTranslationProvider(string btfFile)
        {
            foreach (var trnaslation in BTF.LoadBtf(btfFile))
            {
                _translations.Add(trnaslation.Id, trnaslation.Text);
            }
        }

        public string GetString(int id)
        {
            if (_translations.TryGetValue(id, out var text))
            {
                return text;
            }

            return id.ToString();
        }
    }
}
