using translord.Enums;

namespace translord.Core;

public record FileGetterOptions(string TranslationsPath);

public sealed class FileGetter(FileGetterOptions options) : ITranslationsGetter
{
    TranslatorConfiguration? ITranslationsGetter.Config { get; set; }
    private string TranslationsPath { get; } = options.TranslationsPath;
    private IDictionary<Language, string> TranslationsCache { get; } = new Dictionary<Language, string>();

    public async Task<string> GetSerializedTranslations(Language language)
    {
        if (((ITranslationsGetter)this).Config is { IsCachingEnabled: true } && TranslationsCache.TryGetValue(language, out var json))
        {
            return json;
        }
        var filePath = $@"{TranslationsPath}/translations.{language.GetISOCode()}.json";
        var serializedJson = await File.ReadAllTextAsync(filePath);
        TranslationsCache[language] = serializedJson;
        return serializedJson;
    }
}