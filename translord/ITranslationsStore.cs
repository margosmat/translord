using translord.Enums;

namespace translord;

public interface ITranslationsStore
{
    Task<string> GetSerializedTranslations(Language language);
    TranslatorConfiguration? Config { get; set; }
}