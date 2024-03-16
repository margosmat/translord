using Microsoft.EntityFrameworkCore;
using translord.EntityFramework.Data;

namespace translord.EntityFramework.Postgres;

internal class TranslationsPostgresDbContext : TranslationsDbContext
{
    private readonly string _connectionString = "";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);
}