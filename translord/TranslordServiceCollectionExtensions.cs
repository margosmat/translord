using Microsoft.Extensions.DependencyInjection;

namespace translord;

public static class TranslordServiceCollectionExtensions
{
    public static IServiceCollection AddTranslord(this IServiceCollection services, Action<TranslatorConfigurationOptions> setupOptions)
    {
        var options = new TranslatorConfigurationOptions();
        setupOptions(options);
        services.AddTransient<ITranslator>(x =>
        {
            var config = new TranslatorConfiguration(options, x.GetRequiredService<ITranslationsGetter>());
            return config.CreateTranslator();
        });
        return services;
    }
    
    public static IServiceCollection AddTranslordCustomGetter<T>(this IServiceCollection services) where T : class, ITranslationsGetter
    {
        services.AddTransient<ITranslationsGetter, T>();
        return services;
    }
}