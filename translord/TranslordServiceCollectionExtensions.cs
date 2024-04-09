using Microsoft.Extensions.DependencyInjection;
using translord.Core;

namespace translord;

public static class TranslordServiceCollectionExtensions
{
    public static IServiceCollection AddTranslord(this IServiceCollection services, Action<TranslatorConfigurationOptions> setupOptions)
    {
        var options = new TranslatorConfigurationOptions();
        setupOptions(options);
        services.AddTransient<ITranslator>(x =>
        {
            var config = new TranslatorConfiguration(options, x.GetRequiredService<ITranslationsStore>());
            return config.CreateTranslator();
        });
        return services;
    }
    
    public static IServiceCollection AddTranslordCustomStore<T>(this IServiceCollection services) where T : class, ITranslationsStore
    {
        services.AddTransient<ITranslationsStore, T>();
        return services;
    }
    
    public static IServiceCollection AddTranslordFileStore(this IServiceCollection services, Action<FileStoreOptions> setupOptions)
    {
        var options = new FileStoreOptions();
        setupOptions(options);
        services.AddTransient<ITranslationsStore>(_ => new FileStore(options));
        return services;
    }
}