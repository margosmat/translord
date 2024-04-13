using translord.Enums;
using translord.Models;

namespace translord;

public interface ITranslator
{
    Task<string> GetTranslation(string key, Language language);
    Task<IList<Translation>> GetAllTranslations(Language? language = null);
    Task<string> GetAllTranslationsRawJson(Language language);
    List<Language> GetSupportedLanguages();
    Task SaveTranslation(string key, Language language, string value);
    Task RemoveTranslation(string key);
}