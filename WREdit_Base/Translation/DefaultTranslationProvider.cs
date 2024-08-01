namespace WREdit.Base.Translation
{
    public class DefaultTranslationProvider : ITranslationProvider
    {
        public string GetString(int id) => id.ToString();
    }
}
