using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using translord.Enums;
using translord.Models;

namespace translord.Core;

internal sealed class Translator(TranslatorConfiguration config, ITranslationsStore translationsStore) : ITranslator
{
    private TranslatorConfiguration Config { get; } = config;
    private ITranslationsStore TranslationsStore { get; } = translationsStore;

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
                var allLanguagesTranslationsTasks =
                    Config.SupportedLanguages.Select(async x => await GetSingleLanguageTranslations(allKeys, x));
                var allLanguagesTranslations = await Task.WhenAll(allLanguagesTranslationsTasks);
                translations = allLanguagesTranslations.SelectMany(x => x).ToList();
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
            var value = matchingElement.Value.ValueKind == JsonValueKind.Undefined ? String.Empty : matchingElement.Value.GetString();
            return new Translation
            {
                Language = language,
                Key = x,
                Value = value ?? string.Empty
            };
        }).ToList();
    }

    public Task<string> GetAllTranslationsRawJson(Language language)
    {
        try
        {
            return TranslationsStore.GetSerializedTranslations(language);
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
}