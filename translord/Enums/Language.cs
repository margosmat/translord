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
        switch (language)
        {
            case Language.EnglishBritish: return "en-gb";
            case Language.Polish: return "pl";
            case Language.German: return "de";
            case Language.French: return "fr";
            case Language.Spanish: return "es";
            case Language.Italian: return "it";
            case Language.Ukrainian: return "uk";
            case Language.Czech: return "cs";
            case Language.Japanese: return "ja";
            case Language.Bulgarian: return "bg";
            case Language.Danish: return "da";
            case Language.Greek: return "el";
            case Language.EnglishAmerican: return "en-us";
            case Language.Estonian: return "et";
            case Language.Finnish: return "fi";
            case Language.Hungarian: return "hu";
            case Language.Indonesian: return "id";
            case Language.Korean: return "ko";
            case Language.Lithuanian: return "lt";
            case Language.Latvian: return "lv";
            case Language.Norwegian: return "nb";
            case Language.Dutch: return "nl";
            case Language.PortugueseBrazilian: return "pt-br";
            case Language.PortugueseEuropean: return "pt-pt";
            case Language.Romanian: return "ro";
            case Language.Russian: return "ru";
            case Language.Slovak: return "sk";
            case Language.Slovenian: return "sl";
            case Language.Swedish: return "sv";
            case Language.Turkish: return "tr";
            case Language.ChineseSimplified: return "zh";
            default: throw new NotImplementedException("Language is not implemented yet.");
        }
    }
    
    public static string GetSourceIsoCode(this Language language)
    {
        switch (language)
        {
            case Language.EnglishBritish:
            case Language.EnglishAmerican: return "en";
            case Language.PortugueseBrazilian:
            case Language.PortugueseEuropean: return "pt";
            default: return GetIsoCode(language);
        }
    }
    
    public static string GetName(this Language language)
    {
        switch (language)
        {
            case Language.EnglishBritish: return "English (British)";
            case Language.Polish: return "Polish";
            case Language.German: return "German";
            case Language.French: return "French";
            case Language.Spanish: return "Spanish";
            case Language.Italian: return "Italian";
            case Language.Ukrainian: return "Ukrainian";
            case Language.Czech: return "Czech";
            case Language.Japanese: return "Japanese";
            case Language.Bulgarian: return "Bulgarian";
            case Language.Danish: return "Danish";
            case Language.Greek: return "Greek";
            case Language.EnglishAmerican: return "English (American)";
            case Language.Estonian: return "Estonian";
            case Language.Finnish: return "Finnish";
            case Language.Hungarian: return "Hungarian";
            case Language.Indonesian: return "Indonesian";
            case Language.Korean: return "Korean";
            case Language.Lithuanian: return "Lithuanian";
            case Language.Latvian: return "Latvian";
            case Language.Norwegian: return "Norwegian";
            case Language.Dutch: return "Dutch";
            case Language.PortugueseBrazilian: return "Portuguese (Brazilian)";
            case Language.PortugueseEuropean: return "Portuguese (European)";
            case Language.Romanian: return "Romanian";
            case Language.Russian: return "Russian";
            case Language.Slovak: return "Slovak";
            case Language.Slovenian: return "Slovenian";
            case Language.Swedish: return "Swedish";
            case Language.Turkish: return "Turkish";
            case Language.ChineseSimplified: return "Chinese (Simplified)";
            default: throw new NotImplementedException("Language is not implemented yet.");
        }
    }
}