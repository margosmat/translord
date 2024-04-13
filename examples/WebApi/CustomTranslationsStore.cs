using System.Text.Json;
using translord;
using translord.Enums;

namespace WebApi;

public class CustomTranslationsStore : ITranslationsStore
{
    public Task<string> GetSerializedTranslations(Language language)
    {
        switch (language)
        {
            case Language.English:
                return Task.FromResult(JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    {"label.test", "Test"},
                    {"label.hello", "Hello"}
                }));
            case Language.Polish:
                return Task.FromResult(JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    {"label.test", "Testowanko"},
                    {"label.hello", "Hejo"}
                }));
            case Language.German:
            default:
                return Task.FromResult(string.Empty);
        }
    }

    public Task<List<string>> GetAllKeys()
    {
        return Task.FromResult(new List<string> {"label.test", "label.hello"});
    }

    TranslatorConfiguration? ITranslationsStore.Config { get; set; }
    public Task SaveTranslation(string key, Language language, string value)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTranslation(string key)
    {
        throw new NotImplementedException();
    }
}