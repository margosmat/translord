using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using translord.EntityFramework.Data;
using translord.Enums;

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
        var translations = _context.Translations.Where(x => x.Language == language);
        var json = JsonSerializer.Serialize(await translations.ToDictionaryAsync(x => x.Key, x => x.Value));
        return json;
    }
}