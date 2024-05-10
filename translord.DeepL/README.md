## translord.DeepL
translord.DeepL contains the logic responsible for translating texts using the [DeepL API](https://www.deepl.com/pro-api?cta=header-pro-api). To use this package you will need to register and obtain their API key. You can register with free account and use 500k characters translations per month.

## Configuration
### Web DI
```c#
builder.Services.AddTranslordDeepLTranslator(options =>
{
    options.AuthKey = builder.Configuration["DeepLAuthKey"]; // Remember to add auth key to your config, or add it from different place (KeyVault/etc.)
});
```

### Console app
```c#
var deeplTranslator = new DeepLTranslator(new AddTranslordDeepLTranslatorOptions { AuthKey = "your-auth-key" });
var translator =
    new TranslatorConfiguration(
        new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, DefaultLanguage = Language.English },
        new FileStore(new FileStoreOptions { TranslationsPath = path }, null),
        deeplTranslator).CreateTranslator();
```