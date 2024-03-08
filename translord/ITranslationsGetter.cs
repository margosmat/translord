using translord.Enums;

namespace translord;

public interface ITranslationsGetter
{
    Task<string> GetSerializedTranslations(Language language);
    internal TranslatorConfiguration? Config { get; set; }
}