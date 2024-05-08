using System.Text.Json;
using translord.Enums;
using translord.Models;

namespace translord.Core;

internal sealed class Translator(
    TranslatorConfiguration config,
    ITranslationsStore translationsStore,
    ILanguageTranslator? languageTranslator) : ITranslator
{
    private TranslatorConfiguration Config { get; } = config;
    private ITranslationsStore TranslationsStore { get; } = translationsStore;
    public bool IsTranslationSupported { get; } = languageTranslator is not null;

    public async Task<string> GetTranslation(string key, Language language)
    {
        try
        {
            var json = await TranslationsStore.GetSerializedTranslations(language);
            var deserializedJson = JsonSerializer.Deserialize<JsonElement>(json);
            if (deserializedJson.TryGetProperty(key, out var value))
            {
                return value.GetString() ?? "";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return string.Empty;
    }

    public async Task<IList<Translation>> GetAllTranslations(Language? language)
    {
        List<Translation> translations;
        try
        {
            var allKeys = await TranslationsStore.GetAllKeys();
            if (language.HasValue)
            {
                translations = await GetSingleLanguageTranslations(allKeys, language.Value);
            }
            else
            {
                var allLanguagesTranslations = new List<Translation>();
                foreach (var supportedLanguage in Config.SupportedLanguages)
                {
                    var langTranslations = await GetSingleLanguageTranslations(allKeys, supportedLanguage);
                    allLanguagesTranslations.AddRange(langTranslations);
                }

                translations = allLanguagesTranslations.ToList();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return translations;
    }

    private async Task<List<Translation>> GetSingleLanguageTranslations(List<string> allKeys, Language language)
    {
        var json = await TranslationsStore.GetSerializedTranslations(language);
        var jsonElements = new List<JsonProperty>();

        if (!json.Equals(String.Empty))
        {
            var deserializedJson = JsonSerializer.Deserialize<JsonElement>(json);
            jsonElements = deserializedJson.EnumerateObject().ToList();
        }

        return allKeys.Select(x =>
        {
            var matchingElement = jsonElements.Find(y => y.Name.Equals(x));
            var value = matchingElement.Value.ValueKind == JsonValueKind.Undefined
                ? String.Empty
                : matchingElement.Value.GetString();
            return new Translation
            {
                Language = language,
                Key = x,
                Value = value ?? string.Empty
            };
        }).ToList();
    }

    public async Task<string> GetAllTranslationsRawJson(Language language)
    {
        try
        {
            return await TranslationsStore.GetSerializedTranslations(language);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Language> GetSupportedLanguages()
    {
        return Config.SupportedLanguages.ToList();
    }

    public Language? GetDefaultLanguage()
    {
        return Config.DefaultLanguage;
    }

    public async Task SaveTranslation(string key, Language language, string value)
    {
        await TranslationsStore.SaveTranslation(key, language, value);
    }

    public async Task RemoveTranslation(string key)
    {
        await TranslationsStore.RemoveTranslation(key);
    }

    public async Task<List<(Language lang, int count)>> GetTranslationsCount()
    {
        return await TranslationsStore.GetTranslationsCount();
    }

    public async Task<string> Translate(string text, Language from, Language to)
    {
        return await languageTranslator!.Translate(text, from, to);
    }

    public async Task<List<string>> Translate(string text, Language from, List<Language> to)
    {
        return await languageTranslator!.Translate(text, from, to);
    }

    public async Task ImportTranslations(JsonDocument json, Language language)
    {
        var translations = json.RootElement
            .EnumerateObject()
            .Select(p => new Translation
            {
                Key = p.Name,
                Value = p.Value.GetString() ?? string.Empty,
                Language = language
            }).Where(x => !string.IsNullOrEmpty(x.Value)).ToList();
        foreach (var translation in translations)
        {
            await TranslationsStore.SaveTranslation(translation.Key, translation.Language, translation.Value);
        }
    }
}