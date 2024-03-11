using Microsoft.EntityFrameworkCore;
using translord.Models;

namespace translord.EntityFramework.Data;

public class TranslationsDbContext : DbContext
{
    public DbSet<Translation> Translations { get; set; }
}