using translord.Core;
using translord.Enums;

namespace translord;

public class TranslatorConfiguration(IList<Language> supportedLanguages, string translationsPath)
{
    public IList<Language> SupportedLanguages { get; } = supportedLanguages;
    public string TranslationsPath { get; } = translationsPath;

    public ITranslator CreateTranslator()
    {
        return new Translator();
    }
}