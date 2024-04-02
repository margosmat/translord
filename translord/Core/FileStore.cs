using System.Text.Json;
using translord.Enums;

namespace translord.Core;

public record FileStoreOptions(string TranslationsPath);

public sealed class FileStore(FileStoreOptions options) : ITranslationsStore
{
    TranslatorConfiguration? ITranslationsStore.Config { get; set; }
    private string TranslationsPath { get; } = options.TranslationsPath;
    private IDictionary<Language, string> TranslationsCache { get; } = new Dictionary<Language, string>();

    public async Task<string> GetSerializedTranslations(Language language)
    {
        if (((ITranslationsStore)this).Config is { IsCachingEnabled: true } && TranslationsCache.TryGetValue(language, out var json))
        {
            return json;
        }
        var filePath = $@"{TranslationsPath}/translations.{language.GetISOCode()}.json";
        var serializedJson = await File.ReadAllTextAsync(filePath);
        TranslationsCache[language] = serializedJson;
        return serializedJson;
    }

    public Task<List<string>> GetAllKeys()
    {
        var keys = new List<string>();
        var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
        if (configSupportedLanguages != null)
        {
            foreach (var lang in configSupportedLanguages)
            {
                var filePath = $@"{TranslationsPath}/translations.{lang.GetISOCode()}.json";
                if (!File.Exists(filePath)) continue;
                using var fs = new FileStream(filePath, FileMode.Open);
                using var document = JsonDocument.Parse(fs);

                var names = document.RootElement
                    .EnumerateArray()
                    .SelectMany(o => o.EnumerateObject())
                    .Select(p => p.Name);
                keys.AddRange(names);
            }
        }

        return Task.FromResult(keys.Distinct().ToList());
    }
}