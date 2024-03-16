using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using translord.EntityFramework.Data;
using translord.Enums;

namespace translord.EntityFramework;

public class EfGetter : ITranslationsGetter
{
    private readonly TranslationsDbContext _context;
    TranslatorConfiguration? ITranslationsGetter.Config { get; set; }

    public EfGetter(TranslationsDbContext context)
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