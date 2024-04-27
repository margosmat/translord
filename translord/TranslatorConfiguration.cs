using translord.Core;
using translord.Enums;

namespace translord;

public record TranslatorConfigurationOptions
{
    public IList<Language> SupportedLanguages { get; set; }
    public Language? DefaultLanguage { get; set; }
}

public class TranslatorConfiguration(
    TranslatorConfigurationOptions options,
    ITranslationsStore store,
    ILanguageTranslator? languageTranslator = null)
{
    public IList<Language> SupportedLanguages { get; } = options.SupportedLanguages;
    public Language? DefaultLanguage { get; } = options.DefaultLanguage;
    private ITranslationsStore TranslationsStore { get; } = store;
    private ILanguageTranslator? LanguageTranslator { get; } = languageTranslator;

    public ITranslator CreateTranslator()
    {
        TranslationsStore.Config = this;
        return new Translator(this, TranslationsStore, LanguageTranslator);
    }
}