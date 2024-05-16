## translord.RedisCache
translord.RedisCache contains the configuration of the translations cache using Redis.

## Configuration
### Web DI
```c#
builder.Services.AddTranslordRedisCache(x =>
{
    x.Server = "localhost";
    x.Port = 6379;
    x.Password = "password";
});
```