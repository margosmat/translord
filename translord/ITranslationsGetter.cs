using translord.Enums;

namespace translord;

public interface ITranslationsGetter
{
    Task<string> GetSerializedTranslations(Language language);
    TranslatorConfiguration? Config { get; set; }
}