// See https://aka.ms/new-console-template for more information

using translord;
using translord.Core;
using translord.DeepL;
using translord.Enums;

List<Language> supportedLanguages = [Language.EnglishBritish, Language.Polish, Language.German, Language.Estonian];
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var deeplTranslator = new DeepLTranslator(new AddTranslordDeepLTranslatorOptions { AuthKey = "your-auth-key" });
var translator =
    new TranslatorConfiguration(
        new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, DefaultLanguage = Language.Polish },
        new FileStore(new FileStoreOptions { TranslationsPath = path }, null),
        deeplTranslator).CreateTranslator();

var translations = await translator.GetAllTranslations();
var groupedTranslations = translations.GroupBy(x => x.Language).ToList();
var defaultLanguageTranslations = translations.Where(x => x.Language == translator.GetDefaultLanguage().Value).ToList();

foreach (var translation in groupedTranslations)
{
    var missingTranslations = defaultLanguageTranslations.Select(x => x.Key).Where(x => string.IsNullOrEmpty(translation.FirstOrDefault(y => y.Key == x).Value)).ToList();
    if (missingTranslations.Any())
    {
        foreach (var missingTranslation in missingTranslations)
        {
            var translationValue = await translator.Translate(defaultLanguageTranslations.First(x => x.Key == missingTranslation).Value, translator.GetDefaultLanguage().Value, translation.Key);
            await translator.SaveTranslation(missingTranslation, translation.Key, translationValue);
        }
    }
}

while (true)
{
    Console.WriteLine($"Enter language code to translate: ({supportedLanguages.Select(x => x.GetIsoCode()).Aggregate((x, y) => $"{x}, {y}")})");
    var key = Console.ReadLine();
    if (!supportedLanguages.Select(x => x.GetIsoCode()).Contains(key.ToLower()))
    {
        Console.WriteLine("Invalid language code.");
        continue;
    }

    var translationP1 = await translator.GetTranslation("message.paragraph1", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP1);

    if (translator.GetDefaultLanguage().HasValue && translator.GetDefaultLanguage().Value.GetIsoCode() == key.ToLower())
    {
        var translationP2 = await translator.GetTranslation("message.paragraph2", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
        Console.WriteLine(translationP2);
    }
    
    var translationP3 = await translator.GetTranslation("message.paragraph3", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP3);

    var translationP4 = await translator.GetTranslation("message.paragraph4", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP4);

    var translationP5 = await translator.GetTranslation("message.paragraph5", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP5);

    var translationP6 = await translator.GetTranslation("message.paragraph6", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP6);

    var translationP7 = await translator.GetTranslation("message.paragraph7", supportedLanguages.First(x => x.GetIsoCode() == key.ToLower()));
    Console.WriteLine(translationP7);
}
