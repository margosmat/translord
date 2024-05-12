using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using translord.EntityFramework.Data;

namespace translord.EntityFramework.Postgres;

public record AddTranslordPostgresStoreOptions
{
    public string? ConnectionString { get; set; }
}

public static class TranslordEntityFrameworkPostgresServiceCollectionExtensions
{
    public static IServiceCollection AddTranslordPostgresStore(this IServiceCollection services,
        Action<AddTranslordPostgresStoreOptions> setupAction)
    {
        var options = new AddTranslordPostgresStoreOptions();
        setupAction(options);
        var connectionString = options.ConnectionString ?? throw new ArgumentNullException(nameof(options.ConnectionString));
        services.AddDbContext<TranslationsDbContext>(o => o.UseNpgsql(connectionString));
        services.AddTranslordEfStore();
        return services;
    }
}