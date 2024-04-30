using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using translord.EntityFramework.Data;
using translord.Enums;
using translord.Models;

namespace translord.EntityFramework;

internal class EfStore : ITranslationsStore
{
    private readonly TranslationsDbContext _context;
    private readonly ITranslationsCache? _cache;

    TranslatorConfiguration? ITranslationsStore.Config { get; set; }

    public EfStore(TranslationsDbContext context, ITranslationsCache? cache)
    {
        _context = context;
        _cache = cache;
    }
    
    public async Task<string> GetSerializedTranslations(Language language)
    {
        if (_cache is not null)
        {
            var json = await _cache.Get($"{language}");
            if (!string.IsNullOrEmpty(json)) return json;
        }

        var translations = await _context.Translations.Where(x => x.Language == language).ToDictionaryAsync(x => x.Key, x => x.Value);
        var serializedJson = JsonSerializer.Serialize(translations);
        if (_cache is not null) await _cache.Add($"{language}", serializedJson);
        return serializedJson;
    }
    
    public async Task<List<string>> GetAllKeys()
    {
        var allKeys = await _context.Translations.Select(x => x.Key).Distinct().ToListAsync();
        return allKeys;
    }
    
    public async Task SaveTranslation(string key, Language language, string value)
    {
        var existingTranslation = await _context.Translations.FirstOrDefaultAsync(x => x.Language == language && x.Key == key);
        if (existingTranslation is not null)
        {
            existingTranslation.Value = value;
            _context.Translations.Update(existingTranslation);
        }
        else
        {
            _context.Translations.Add(new Translation { Key = key, Value = value, Language = language });
        }

        await _context.SaveChangesAsync();

        if (_cache is not null) await _cache.Remove($"{language}");
    }

    public async Task RemoveTranslation(string key)
    {
        var translationsToRemove = await _context.Translations.Where(x => x.Key == key).ToListAsync();
        if (translationsToRemove.Any())
        {
            _context.Translations.RemoveRange(translationsToRemove);
            await _context.SaveChangesAsync();
            foreach (var lang in translationsToRemove.Select(x => x.Language).Distinct())
            {
                if (_cache is not null) await _cache.Remove($"{lang}");
            }
        }
    }

    public async Task<List<(Language lang, int count)>> GetTranslationsCount()
    {
        var translationsCount = new List<(Language lang, int count)>();
        var configSupportedLanguages = ((ITranslationsStore)this).Config?.SupportedLanguages;
        if (configSupportedLanguages == null) return translationsCount;
        foreach (var lang in configSupportedLanguages)
        {
            var count = await _context.Translations.CountAsync(x => x.Language == lang);
            translationsCount.Add((lang, count));
        }

        return translationsCount;
    }
}