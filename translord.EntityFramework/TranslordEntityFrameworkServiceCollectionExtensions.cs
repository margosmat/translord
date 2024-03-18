using Microsoft.Extensions.DependencyInjection;

namespace translord.EntityFramework;

public static class TranslordEntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddTranslordEfStore(this IServiceCollection services)
    {
        services.AddTransient<ITranslationsStore, EfStore>();
        return services;
    }
}