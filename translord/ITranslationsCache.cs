namespace translord;

public interface ITranslationsCache
{
    Task Add(string key, string value);
    Task<string?> Get(string key);
    Task Remove(string key);
    Task RemoveAll(List<string> keys);
}