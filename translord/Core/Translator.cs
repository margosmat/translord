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

    public async Task<IList<Translation>> GetAllTranslations(Language language)
    {
        List<Translation> translations;
        try
        {
            var json = await TranslationsStore.GetSerializedTranslations(language);
            var deserializedJson = JsonSerializer.Deserialize<JsonElement>(json);
            var enumerator = deserializedJson.EnumerateObject();

            translations = enumerator.Select(x => new Translation
            {
                Language = language,
                Key = x.Name,
                Value = x.Value.GetString() ?? ""
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return translations;
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