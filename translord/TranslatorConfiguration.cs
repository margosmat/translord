using translord.Core;
using translord.Enums;

namespace translord;

public record TranslatorConfigurationOptions
{
    public IList<Language> SupportedLanguages { get; set; }
    public bool IsCachingEnabled { get; set; }
}

public class TranslatorConfiguration(TranslatorConfigurationOptions options, ITranslationsGetter getter)
{
    public IList<Language> SupportedLanguages { get; } = options.SupportedLanguages;
    public bool IsCachingEnabled { get; } = options.IsCachingEnabled;
    private ITranslationsGetter TranslationsGetter { get; } = getter;

    public ITranslator CreateTranslator()
    {
        TranslationsGetter.Config = this;
        return new Translator(this, TranslationsGetter);
    }
}