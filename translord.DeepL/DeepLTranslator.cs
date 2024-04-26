using translord.Enums;

namespace translord.DeepL;

public class DeepLTranslator : ILanguageTranslator
{
    public Task<string> Translate(string text, Language from, Language to)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> Translate(string text, Language from, List<Language> to)
    {
        throw new NotImplementedException();
    }

    public Task<(Language lang, List<string> translations)> Translate(List<string> text, Language from, List<Language> to)
    {
        throw new NotImplementedException();
    }
}