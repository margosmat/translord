using translord.Core;
using translord.Enums;

namespace translord;

public class TranslatorConfiguration(IList<Language> supportedLanguages, string translationsPath, bool isCachingEnabled = true)
{
    public IList<Language> SupportedLanguages { get; } = supportedLanguages;
    public string TranslationsPath { get; } = translationsPath;
    public bool IsCachingEnabled { get; } = isCachingEnabled;

    public ITranslator CreateTranslator()
    {
        return new Translator(this, new TranslationsGetter(this));
    }
}