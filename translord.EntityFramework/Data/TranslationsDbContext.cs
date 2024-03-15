using Microsoft.EntityFrameworkCore;
using translord.Models;

namespace translord.EntityFramework.Data;

public class TranslationsDbContext : DbContext
{
    public DbSet<Translation> Translations { get; set; }

    public TranslationsDbContext() : base()
    {
        this.Database.EnsureCreated();
    }

    public TranslationsDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TranslationsDbContext).Assembly);
    }
}