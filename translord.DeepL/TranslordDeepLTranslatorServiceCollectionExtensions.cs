using Microsoft.Extensions.DependencyInjection;

namespace translord.DeepL;

public static class TranslordDeepLTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddTranslordDeepLTranslator(this IServiceCollection services,
        Action<AddTranslordDeepLTranslatorOptions> setupAction)
    {
        var options = new AddTranslordDeepLTranslatorOptions();
        setupAction(options);
        services.AddTransient<ILanguageTranslator>(x => new DeepLTranslator(options));
        return services;
    }
}