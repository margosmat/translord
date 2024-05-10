using DeepL;
using translord.Enums;

namespace translord.DeepL;

public record AddTranslordDeepLTranslatorOptions
{
    public string AuthKey { get; set; }
}

public sealed class DeepLTranslator : ILanguageTranslator
{
    private readonly Translator _translator;
    
    public DeepLTranslator(AddTranslordDeepLTranslatorOptions options)
    {
        _translator = new Translator(options.AuthKey);
    }
    
    public async Task<string> Translate(string text, Language from, Language to)
    {
        var result = await _translator.TranslateTextAsync([text], from.GetISOCode(), to.GetISOCode());
        return result[0].Text;
    }

    public async Task<List<string>> Translate(string text, Language from, List<Language> to)
    {
        var translations = new List<string>();
        foreach (var lang in to)
        {
            var result = await _translator.TranslateTextAsync([text], from.GetISOCode(), lang.GetISOCode());
            translations.Add(result[0].Text);
        }
        
        return translations;
    }

    public Task<(Language lang, List<string> translations)> Translate(List<string> text, Language from,
        List<Language> to)
    {
        throw new NotImplementedException();
    }
}