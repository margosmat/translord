using translord.Enums;

namespace translord;

public interface ITranslationsStore
{
    Task<string> GetSerializedTranslations(Language language);
    Task<List<string>> GetAllKeys();
    TranslatorConfiguration? Config { get; set; }
}