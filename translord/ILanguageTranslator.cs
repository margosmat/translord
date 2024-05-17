using translord.Enums;

namespace translord;

public interface ILanguageTranslator
{
    Task<string> Translate(string text, Language from, Language to);
    Task<List<string>> Translate(string text, Language from, List<Language> to);
}