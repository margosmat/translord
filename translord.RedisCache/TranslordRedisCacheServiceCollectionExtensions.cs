using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace translord.RedisCache;

public record AddTranslordRedisCacheOptions
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}

public static class TranslordRedisCacheServiceCollectionExtensions
{
    public static IServiceCollection AddTranslordRedisCache(this IServiceCollection services,
        Action<AddTranslordRedisCacheOptions> setupAction)
    {
        var options = new AddTranslordRedisCacheOptions();
        setupAction(options);
        var configuration = new ConfigurationOptions
        {
            EndPoints = new EndPointCollection
            {
                { options.Server, options.Port }
            },
            Password = options.Password
        };
        services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(configuration));
        services.AddTransient<ITranslationsCache, TranslordRedisCache>();
        return services;
    }
}