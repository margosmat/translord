namespace translord.Enums;

public enum Language
{
    English = 1,
    Polish = 2,
    German = 3,
    French = 4,
    Spanish = 5,
    Italian = 6,
    Ukrainian = 7,
    Czech = 8
}

public static class LanguageExtensions
{
    public static string GetISOCode(this Language language)
    {
        switch (language)
        {
            case Language.English: return "en";
            case Language.Polish: return "pl";
            case Language.German: return "de";
            case Language.French: return "fr";
            case Language.Spanish: return "es";
            case Language.Italian: return "it";
            case Language.Ukrainian: return "uk";
            case Language.Czech: return "cs";
            default: throw new NotImplementedException("Language is not implemented yet.");
        }
    }
    
    public static string GetName(this Language language)
    {
        switch (language)
        {
            case Language.English: return "English";
            case Language.Polish: return "Polish";
            case Language.German: return "German";
            case Language.French: return "French";
            case Language.Spanish: return "Spanish";
            case Language.Italian: return "Italian";
            case Language.Ukrainian: return "Ukrainian";
            case Language.Czech: return "Czech";
            default: throw new NotImplementedException("Language is not implemented yet.");
        }
    }
}