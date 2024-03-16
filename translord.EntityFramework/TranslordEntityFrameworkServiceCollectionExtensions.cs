using Microsoft.Extensions.DependencyInjection;

namespace translord.EntityFramework;

public static class TranslordEntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddTranslordEfGetter(this IServiceCollection services)
    {
        services.AddTransient<ITranslationsGetter, EfGetter>();
        return services;
    }
}