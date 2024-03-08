using translord.Core;
using translord.Enums;

namespace translord;

public class TranslatorConfiguration(IList<Language> supportedLanguages, ITranslationsGetter getter, bool isCachingEnabled = true)
{
    public IList<Language> SupportedLanguages { get; } = supportedLanguages;
    public bool IsCachingEnabled { get; } = isCachingEnabled;
    private ITranslationsGetter TranslationsGetter { get; } = getter;

    public ITranslator CreateTranslator()
    {
        TranslationsGetter.Config = this;
        return new Translator(this, TranslationsGetter);
    }
}