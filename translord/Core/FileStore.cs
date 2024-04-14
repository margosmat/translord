using System.Text.Json;
using System.Text.Json.Nodes;
using translord.Enums;

namespace translord.Core;

public record FileStoreOptions
{
    public string TranslationsPath { get; set; }
}

public sealed class FileStore(FileStoreOptions options) : ITranslationsStore
{
    TranslatorConfiguration? ITranslationsStore.Config { get; set; }

    private string TranslationsPath { get; } = options.TranslationsPath;
    private IDictionary<Language, string> TranslationsCache { get; } = new Dictionary<Language, string>();

    public async Task<string> GetSerializedTranslations(Language language)
    {
        if (((ITranslationsStore)this).Config is { IsCachingEnabled: true, IsCacheDirty: false } &&
            TranslationsCache.TryGetValue(language, out var json))
        {
            return json;
        }

        var filePath = $@"{TranslationsPath}/translations.{language.GetISOCode()}.json";
        if (!File.Exists(filePath)) return string.Empty;
        var serializedJson = await File.ReadAllTextAsync(filePath);
        TranslationsCache[language] = serializedJson;
        ((ITranslationsStore)this).Config?.MarkCacheClean();
        return serializedJson;
    }

    public async Task<List<string>> GetAllKeys()
    {
        var keys = new List<string>();
        var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
        if (configSupportedLanguages == null) return keys;
        foreach (var lang in configSupportedLanguages)
        {
            var filePath = $@"{TranslationsPath}/translations.{lang.GetISOCode()}.json";
            if (!File.Exists(filePath)) continue;
            await using var fs = new FileStream(filePath, FileMode.Open);
            using var document = await JsonDocument.ParseAsync(fs);

            var names = document.RootElement
                .EnumerateObject()
                .Select(p => p.Name);
            keys.AddRange(names);
        }

        return keys.Distinct().ToList();
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
        ((ITranslationsStore)this).Config?.MarkCacheDirty();
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
        }
    }
}