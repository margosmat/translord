using DeepL;
using translord.Enums;

namespace translord.DeepL;

public record AddTranslordDeepLTranslatorOptions
{
    public string? AuthKey { get; set; }
}

public sealed class DeepLTranslator : ILanguageTranslator
{
    private readonly Translator _translator;
    
    public DeepLTranslator(AddTranslordDeepLTranslatorOptions options)
    {
        var authKey = options.AuthKey ?? throw new ArgumentNullException(nameof(options.AuthKey));
        _translator = new Translator(authKey);
    }
    
    public async Task<string> Translate(string text, Language from, Language to)
    {
        var result = await _translator.TranslateTextAsync([text], from.GetIsoCode(), to.GetIsoCode());
        return result[0].Text;
    }

    public async Task<List<string>> Translate(string text, Language from, List<Language> to)
    {
        var translations = new List<string>();
        foreach (var lang in to)
        {
            var result = await _translator.TranslateTextAsync([text], from.GetIsoCode(), lang.GetIsoCode());
            translations.Add(result[0].Text);
        }
        
        return translations;
    }
}