# translord

translord - simple TMS to get your translations up and running in no time.

🚧 Project still in the early stages of development 

What this tool aims to achieve? To be a central place in your project that handles all things related to your translations:
- storing
- translating
- delivering
- management
- revision

## Packages structure
- translord
    - The core library
- [translord.DeepL](https://github.com/margosmat/translord/tree/main/translord.DeepL)
    - Library containing [DeepL API](https://www.deepl.com/pro-api?cta=header-pro-api) configuration for texts translation in translord.
- [translord.EntityFramework](https://github.com/margosmat/translord/tree/main/translord.EntityFramework)
    - Library containing configuration data that uses EntityFramework as its database abstraction.
- [translord.EntityFramework.Postgres](https://github.com/margosmat/translord/tree/main/translord.EntityFramework.Postgres)
    - Library extending the `translord.EntityFramework` library with Postgres configuration.
- [translord.Manager](https://github.com/margosmat/translord/tree/main/translord.Manager)
    - Library containing the TMS admin panel allowing for translations editing/management/translation.
- [translord.RedisCache](https://github.com/margosmat/translord/tree/main/translord.RedisCache)
    - Library containing configuration for Redis as the cache for translord.

## Configuration examples

### WebApp with FileStore
```c#
builder.Services.AddTranslordFileStore(options =>
{
    options.TranslationsPath = Path.Combine(Directory.GetCurrentDirectory(), "translations");
});
builder.Services.AddTranslord(o =>
{
    List<Language> supportedLanguages = [Language.English, Language.Polish, Language.German];
    o.SupportedLanguages = supportedLanguages;
    o.IsCachingEnabled = true;
});
```

### WebApp with PostgresStore and TMS panel
```c#
builder.Services.AddTranslordPostgresStore(options =>
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddTranslord(o =>
{
    List<Language> supportedLanguages = [Language.English, Language.Polish, Language.German];
    o.SupportedLanguages = supportedLanguages;
    o.IsCachingEnabled = true;
});
builder.AddTranslordManager();
```

### Console app with FileStore
```c#
List<Language> supportedLanguages = [ Language.English, Language.Polish ];
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var deeplTranslator = new DeepLTranslator(new AddTranslordDeepLTranslatorOptions { AuthKey = "your-auth-key" });
var translator =
    new TranslatorConfiguration(
        new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, DefaultLanguage = Language.English },
        new FileStore(new FileStoreOptions { TranslationsPath = path }, null),
        deeplTranslator).CreateTranslator();

var label = await translator.GetTranslation("label.test", Language.Polish);
var translations = await translator.GetAllTranslations(Language.English);
```

### Custom implementations
You can configure your own custom implementations for the **store**, **cache** or **translator** in translord. Just implement specific interface and be sure to register it in DI:
```c#
builder.Services.AddTranslordCustomStore<CustomTranslationsStore>();
builder.Services.AddTranslordCustomCache<CustomTranslationsCache>();
builder.Services.AddTranslordCustomTranslator<CustomTranslationsTranslator>();
```

## Import
For now, you can import your translations in one specific way. You need one `.json` file per language, with root object containing string key-value pairs of your translations. In the future there could be more import schemas added, please add an issue if you need support for specific import schema. Example of json that can be imported now:
```json
{
    "label.add": "add",
    "label.delete": "delete",
    ...
}
```

See [GitHub](https://github.com/margosmat/translord) for more information and examples.