using Microsoft.EntityFrameworkCore;
using translord.Models;

namespace translord.EntityFramework.Data;

public class TranslationsDbContext : DbContext
{
    public required DbSet<Translation> Translations { get; set; }

    public TranslationsDbContext() {}

    public TranslationsDbContext(DbContextOptions<TranslationsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TranslationsDbContext).Assembly);
    }
}