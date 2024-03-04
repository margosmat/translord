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
            var json = GetTranslationsJSON(language);
            if (json.TryGetProperty(key, out var value))
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
            var json = GetTranslationsJSON(language);
            var enumerator = json.EnumerateObject();

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

    private JsonElement GetTranslationsJSON(Language language)
    {
        var filePath = $@"{Config.TranslationsPath}/translations.{language.GetISOCode()}.json";
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<JsonElement>(json);
    }
}