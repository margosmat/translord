using translord.Enums;
using translord.Models;

namespace translord;

public interface ITranslator
{
    Task<string> GetTranslation(string key, Language language);
    Task<IList<Translation>> GetAllTranslations(Language language);
    Task<string> GetAllTranslationsRawJson(Language language);
}