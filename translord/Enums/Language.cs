namespace translord.Enums;

public enum Language
{
    EnglishBritish = 1,
    Polish = 2,
    German = 3,
    French = 4,
    Spanish = 5,
    Italian = 6,
    Ukrainian = 7,
    Czech = 8,
    Japanese = 9,
    Bulgarian = 10,
    Danish = 11,
    Greek = 12,
    EnglishAmerican = 13,
    Estonian = 14,
    Finnish = 15,
    Hungarian = 16,
    Indonesian = 17,
    Korean = 18,
    Lithuanian = 19,
    Latvian = 20,
    Norwegian = 21,
    Dutch = 22,
    PortugueseBrazilian = 23,
    PortugueseEuropean = 24,
    Romanian = 25,
    Russian = 26,
    Slovak = 27,
    Slovenian = 28,
    Swedish = 29,
    Turkish = 30,
    ChineseSimplified = 31,
}

public static class LanguageExtensions
{
    public static string GetIsoCode(this Language language)
    {
        return language switch
        {
            Language.EnglishBritish => "en-gb",
            Language.Polish => "pl",
            Language.German => "de",
            Language.French => "fr",
            Language.Spanish => "es",
            Language.Italian => "it",
            Language.Ukrainian => "uk",
            Language.Czech => "cs",
            Language.Japanese => "ja",
            Language.Bulgarian => "bg",
            Language.Danish => "da",
            Language.Greek => "el",
            Language.EnglishAmerican => "en-us",
            Language.Estonian => "et",
            Language.Finnish => "fi",
            Language.Hungarian => "hu",
            Language.Indonesian => "id",
            Language.Korean => "ko",
            Language.Lithuanian => "lt",
            Language.Latvian => "lv",
            Language.Norwegian => "nb",
            Language.Dutch => "nl",
            Language.PortugueseBrazilian => "pt-br",
            Language.PortugueseEuropean => "pt-pt",
            Language.Romanian => "ro",
            Language.Russian => "ru",
            Language.Slovak => "sk",
            Language.Slovenian => "sl",
            Language.Swedish => "sv",
            Language.Turkish => "tr",
            Language.ChineseSimplified => "zh",
            _ => throw new NotImplementedException("Language is not implemented yet.")
        };
    }
    
    
    public static Language FromIsoCode(this string languageIsoCode)
    {
        return languageIsoCode switch
        {
            "en-gb" => Language.EnglishBritish,
            "pl" => Language.Polish,
            "de" => Language.German,
            "fr" => Language.French,
            "es" => Language.Spanish,
            "it" => Language.Italian,
            "uk" => Language.Ukrainian,
            "cs" => Language.Czech,
            "ja" => Language.Japanese,
            "bg" => Language.Bulgarian,
            "da" => Language.Danish,
            "el" => Language.Greek,
            "en-us" => Language.EnglishAmerican,
            "et" => Language.Estonian,
            "fi" => Language.Finnish,
            "hu" => Language.Hungarian,
            "id" => Language.Indonesian,
            "ko" => Language.Korean,
            "lt" => Language.Lithuanian,
            "lv" => Language.Latvian,
            "nb" => Language.Norwegian,
            "nl" => Language.Dutch,
            "pt-br" => Language.PortugueseBrazilian,
            "pt-pt" => Language.PortugueseEuropean,
            "ro" => Language.Romanian,
            "ru" => Language.Russian,
            "sk" => Language.Slovak,
            "sl" => Language.Slovenian,
            "sv" => Language.Swedish,
            "tr" => Language.Turkish,
            "zh" => Language.ChineseSimplified,
            _ => throw new NotImplementedException("Language is not implemented yet.")
        };
    }
    
    public static string GetSourceIsoCode(this Language language)
    {
        return language switch
        {
            Language.EnglishBritish or Language.EnglishAmerican => "en",
            Language.PortugueseBrazilian or Language.PortugueseEuropean => "pt",
            _ => GetIsoCode(language)
        };
    }
    
    public static string GetName(this Language language)
    {
        return language switch
        {
            Language.EnglishBritish => "English (British)",
            Language.Polish => "Polish",
            Language.German => "German",
            Language.French => "French",
            Language.Spanish => "Spanish",
            Language.Italian => "Italian",
            Language.Ukrainian => "Ukrainian",
            Language.Czech => "Czech",
            Language.Japanese => "Japanese",
            Language.Bulgarian => "Bulgarian",
            Language.Danish => "Danish",
            Language.Greek => "Greek",
            Language.EnglishAmerican => "English (American)",
            Language.Estonian => "Estonian",
            Language.Finnish => "Finnish",
            Language.Hungarian => "Hungarian",
            Language.Indonesian => "Indonesian",
            Language.Korean => "Korean",
            Language.Lithuanian => "Lithuanian",
            Language.Latvian => "Latvian",
            Language.Norwegian => "Norwegian",
            Language.Dutch => "Dutch",
            Language.PortugueseBrazilian => "Portuguese (Brazilian)",
            Language.PortugueseEuropean => "Portuguese (European)",
            Language.Romanian => "Romanian",
            Language.Russian => "Russian",
            Language.Slovak => "Slovak",
            Language.Slovenian => "Slovenian",
            Language.Swedish => "Swedish",
            Language.Turkish => "Turkish",
            Language.ChineseSimplified => "Chinese (Simplified)",
            _ => throw new NotImplementedException("Language is not implemented yet.")
        };
    }
}