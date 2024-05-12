# translord

---

<div align="center">

  ![translord](assets/logo.jpeg)

</div>

---

#### translord - simple TMS to get your translations up and running in no time.

🚧 Project under development 

What this tool aims to achieve? To be a central place in your project that handles all things related to your translations:
- storing
- translating
- delivering
- management
- revision

## Packages
- translord
    - The core library
- [translord.DeepL](https://github.com/margosmat/translord/tree/main/translord.DeepL)
    - Library containing [DeepL API](https://www.deepl.com/pro-api?cta=header-pro-api) configuration for texts translation in translord.
- [translord.EntityFramework](https://github.com/margosmat/translord/tree/main/translord.EntityFramework)
    - Library containing configuration data that uses EntityFramework as its database abstraction.
- [translord.EntityFramework.Postgres](https://github.com/margosmat/translord/tree/main/translord.EntityFramework.Postgres)
    - Library extending the `translord.EntityFramework` library with Postgres configuration.
- translord.Manager
    - Library containing the TMS admin panel allowing for translations editing/management/translation.
- translord.RedisCache
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

## TMS panel

![TMS panel screenshot](assets/panel_screenshot.png)

## Features

### Must have

- [x] modularity
- [x] easy configuration
- [x] storing (EF Core/Postgres/File)
- [x] DeepL translations
- [x] CMS-like panel
- [x] import of existing translations
- [ ] examples (in progress)

### Nice to have

- [ ] AI based translations
- [ ] caching (in progress)
- [ ] role-based access to the CMS panel
- [ ] translations revision

## Inspiration

- IdentityServer (as for config and ease of use)
- Serilog (as for modularity, structure)

## Support

Feel free to add issues with suggestions.