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
            if (language.HasValue)
            {
                translations = await GetSingleLanguageTranslations(language.Value);
            }
            else
            {
                var allLanguagesTranslationsTasks =
                    Config.SupportedLanguages.Select(async x => await GetSingleLanguageTranslations(x));
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

    private async Task<List<Translation>> GetSingleLanguageTranslations(Language language)
    {
        var json = await TranslationsStore.GetSerializedTranslations(language);

        var deserializedJson = JsonSerializer.Deserialize<JsonElement>(json);
        var enumerator = deserializedJson.EnumerateObject();

        return enumerator.Select(x => new Translation
        {
            Language = language,
            Key = x.Name,
            Value = x.Value.GetString() ?? ""
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
}