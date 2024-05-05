using System.Text.Json;
using translord.Enums;
using translord.Models;

namespace translord;

public interface ITranslator
{
    bool IsTranslationSupported { get; }
    Task<string> GetTranslation(string key, Language language);
    Task<IList<Translation>> GetAllTranslations(Language? language = null);
    Task<string> GetAllTranslationsRawJson(Language language);
    List<Language> GetSupportedLanguages();
    Language? GetDefaultLanguage();
    Task SaveTranslation(string key, Language language, string value);
    Task RemoveTranslation(string key);
    Task<List<(Language lang, int count)>> GetTranslationsCount();
    Task<string> Translate(string text, Language from, Language to);
    Task<List<string>> Translate(string text, Language from, List<Language> to);
    Task<(Language lang, List<string> translations)> Translate(List<string> text, Language from, List<Language> to);
    Task ImportTranslations(JsonDocument json, Language language);
}