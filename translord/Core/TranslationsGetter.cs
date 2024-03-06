using translord.Enums;

namespace translord.Core;

internal sealed class TranslationsGetter(TranslatorConfiguration config)
{
    public TranslatorConfiguration Config { get; private set; } = config;
    public IDictionary<Language, string> TranslationsCache { get; } = new Dictionary<Language, string>();

    public async Task<string> GetTranslationsJson(Language language)
    {
        if (Config.IsCachingEnabled && TranslationsCache.TryGetValue(language, out var json))
        {
            return json;
        }
        var filePath = $@"{Config.TranslationsPath}/translations.{language.GetISOCode()}.json";
        var serializedJson = await File.ReadAllTextAsync(filePath);
        TranslationsCache[language] = serializedJson;
        return serializedJson;
    }
}