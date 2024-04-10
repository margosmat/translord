using translord.Core;
using translord.Enums;

namespace translord;

public record TranslatorConfigurationOptions
{
    public IList<Language> SupportedLanguages { get; set; }
    public bool IsCachingEnabled { get; set; }
}

public class TranslatorConfiguration(TranslatorConfigurationOptions options, ITranslationsStore store)
{
    public IList<Language> SupportedLanguages { get; } = options.SupportedLanguages;
    public bool IsCachingEnabled { get; } = options.IsCachingEnabled;
    public bool IsCacheDirty { get; private set; } = false;
    private ITranslationsStore TranslationsStore { get; } = store;

    public ITranslator CreateTranslator()
    {
        TranslationsStore.Config = this;
        return new Translator(this, TranslationsStore);
    }
    
    public void MarkCacheDirty()
    {
        IsCacheDirty = true;
    }
    
    public void MarkCacheClean()
    {
        IsCacheDirty = false;
    }
}