using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using translord.EntityFramework.Data;
using translord.Enums;
using translord.Models;

namespace translord.EntityFramework;

public class EfStore : ITranslationsStore
{
    private readonly TranslationsDbContext _context;

    TranslatorConfiguration? ITranslationsStore.Config { get; set; }

    public EfStore(TranslationsDbContext context)
    {
        _context = context;
    }
    
    public async Task<string> GetSerializedTranslations(Language language)
    {
        var translations = await _context.Translations.Where(x => x.Language == language).ToDictionaryAsync(x => x.Key, x => x.Value);
        var json = JsonSerializer.Serialize(translations);
        return json;
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
    }

    public async Task RemoveTranslation(string key)
    {
        var translationsToRemove = await _context.Translations.Where(x => x.Key == key).ToListAsync();
        if (translationsToRemove.Any())
        {
            _context.Translations.RemoveRange(translationsToRemove);
            await _context.SaveChangesAsync();
        }
    }
}