namespace translord.Enums;

public enum Language
{
    English = 1,
    Polish = 2,
    German = 3
}

public static class LanguageExtensions
{
    public static string GetISOCode(this Language language)
    {
        switch (language)
        {
            case Language.English: return "en";
            case Language.Polish: return "pl";
            default: throw new NotImplementedException("Language is not implemented yet.");
        }
    }
}