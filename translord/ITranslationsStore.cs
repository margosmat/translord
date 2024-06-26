using translord.Enums;

namespace translord;

public interface ITranslationsStore
{
    Task<string> GetSerializedTranslations(Language language);
    Task<List<string>> GetAllKeys();
    TranslatorConfiguration? Config { get; set; }
    Task SaveTranslation(string key, Language language, string value);
    Task RemoveTranslation(string key);
    Task<List<(Language lang, int count)>> GetTranslationsCount();
}