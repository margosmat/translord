using System.Text.Json;
using translord.Enums;
using translord.Models;

namespace translord.Core;

public sealed class Translator(TranslatorConfiguration config) : ITranslator
{
    public TranslatorConfiguration Config { get; } = config;
    public string GetTranslation(string key, Language language)
    {
        try
        {
            var json = GetTranslationsJson(language);
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

    public IList<Translation> GetAllTranslations(Language language)
    {
        List<Translation> translations;
        try
        {
            var json = GetTranslationsJson(language);
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

    public string GetAllTranslationsRawJson(Language language)
    {
        try
        {
            return GetTranslationsJson(language);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string GetTranslationsJson(Language language)
    {
        var filePath = $@"{Config.TranslationsPath}/translations.{language.GetISOCode()}.json";
        return File.ReadAllText(filePath);
    }
}