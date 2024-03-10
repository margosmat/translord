using translord.Enums;

namespace translord.EntityFramework;

public record EfGetterOptions(string ConnectionString);

public class EfGetter : ITranslationsGetter
{
    TranslatorConfiguration? ITranslationsGetter.Config { get; set; }

    public EfGetter(EfGetterOptions options)
    {
        
    }
    
    public Task<string> GetSerializedTranslations(Language language)
    {
        throw new NotImplementedException();
    }
}