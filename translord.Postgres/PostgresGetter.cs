using translord.Enums;

namespace translord.Postgres;

public record PostgresGetterOptions(string ConnectionString);

public class PostgresGetter : ITranslationsGetter
{
    TranslatorConfiguration? ITranslationsGetter.Config { get; set; }

    public PostgresGetter(PostgresGetterOptions options)
    {
        
    }
    
    public Task<string> GetSerializedTranslations(Language language)
    {
        throw new NotImplementedException();
    }
}