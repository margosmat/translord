using translord.Enums;
using translord.Models;

namespace translord;

public interface ITranslator
{
    string GetTranslation(string key, Language language);
    IList<Translation> GetAllTranslations(Language language);
    string GetAllTranslationsRawJson(Language language);
}