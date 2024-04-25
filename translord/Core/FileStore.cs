using System.Text.Json;
using System.Text.Json.Nodes;
using translord.Enums;

namespace translord.Core;

public record FileStoreOptions
{
    public string TranslationsPath { get; set; }
}

public sealed class FileStore(FileStoreOptions options, ITranslationsCache? cache) : ITranslationsStore
{
    TranslatorConfiguration? ITranslationsStore.Config { get; set; }

    private string TranslationsPath { get; } = options.TranslationsPath;

    public async Task<string> GetSerializedTranslations(Language language)
    {
        if (cache is not null)
        {
            var json = await cache.Get($"{language}");
            if (!string.IsNullOrEmpty(json)) return json;
        }

        var filePath = $@"{TranslationsPath}/translations.{language.GetISOCode()}.json";
        if (!File.Exists(filePath)) return string.Empty;
        var serializedJson = await File.ReadAllTextAsync(filePath);
        if (cache is not null) await cache.Add($"{language}", serializedJson);
        return serializedJson;
    }

    public async Task<List<string>> GetAllKeys()
    {
        var keys = new List<string>();
        if (((ITranslationsStore)this).Config?.DefaultLanguage.HasValue ?? false)
        {
            var defaultLanguageKeys = await GetKeysFromLanguageFile(((ITranslationsStore)this).Config.DefaultLanguage.Value);
            keys.AddRange(defaultLanguageKeys);
        }
        else
        {
            var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
            if (configSupportedLanguages == null) return keys;
            foreach (var lang in configSupportedLanguages)
            {
                var names = await GetKeysFromLanguageFile(lang);
                keys.AddRange(names);
            }
        }

        return keys.Distinct().ToList();
    }

    private async Task<IEnumerable<string>> GetKeysFromLanguageFile(Language lang)
    {
        var filePath = $@"{TranslationsPath}/translations.{lang.GetISOCode()}.json";
        if (!File.Exists(filePath)) return Enumerable.Empty<string>();

        await using var fs = new FileStream(filePath, FileMode.Open);
        using var document = await JsonDocument.ParseAsync(fs);

        var names = document.RootElement
            .EnumerateObject()
            .Select(p => p.Name)
            .ToList();

        return names;
    }

    public async Task SaveTranslation(string key, Language language, string value)
    {
        var filePath = $@"{TranslationsPath}/translations.{language.GetISOCode()}.json";
        var options = new JsonSerializerOptions { WriteIndented = true };
        if (!File.Exists(filePath))
        {
            var translationObject = new JsonObject
            {
                [key] = value
            };
            await File.WriteAllTextAsync(filePath, translationObject.ToJsonString(options));
        }
        else
        {
            var jsonString = await File.ReadAllTextAsync(filePath);
            var jsonObject = JsonNode.Parse(jsonString);
            jsonObject![key] = value;
            await File.WriteAllTextAsync(filePath, jsonObject.ToJsonString(options));
        }

        if (cache is not null) await cache.Remove($"{language}");
    }

    public async Task RemoveTranslation(string key)
    {
        var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
        if (configSupportedLanguages == null) return;
        foreach (var lang in configSupportedLanguages)
        {
            var filePath = $@"{TranslationsPath}/translations.{lang.GetISOCode()}.json";
            if (!File.Exists(filePath)) continue;
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = await File.ReadAllTextAsync(filePath);
            var jsonObject = JsonNode.Parse(jsonString);
            if (jsonObject is null) continue;
            (jsonObject as JsonObject)?.Remove(key);
            await File.WriteAllTextAsync(filePath, jsonObject.ToJsonString(options));
            if (cache is not null) await cache.Remove($"{lang}");
        }
    }

    public async Task<List<(Language lang, int count)>> GetTranslationsCount()
    {
        var translationsCount = new List<(Language lang, int count)>();
        var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
        if (configSupportedLanguages == null) return translationsCount;
        foreach (var lang in configSupportedLanguages)
        {
            var filePath = $@"{TranslationsPath}/translations.{lang.GetISOCode()}.json";
            if (!File.Exists(filePath))
            {
                translationsCount.Add((lang, 0));
                continue;
            }

            await using var fs = new FileStream(filePath, FileMode.Open);
            using var document = await JsonDocument.ParseAsync(fs);

            var count = document.RootElement
                .EnumerateObject().Count();
            translationsCount.Add((lang, count));
        }

        return translationsCount;
    }
}