// See https://aka.ms/new-console-template for more information

using Dumpify;
using translord;
using translord.Core;
using translord.DeepL;
using translord.Enums;

List<Language> supportedLanguages = [Language.EnglishBritish, Language.Polish];
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var deeplTranslator = new DeepLTranslator(new AddTranslordDeepLTranslatorOptions { AuthKey = "your-auth-key" });
var translator =
    new TranslatorConfiguration(
        new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, DefaultLanguage = Language.EnglishBritish },
        new FileStore(new FileStoreOptions { TranslationsPath = path }, null),
        deeplTranslator).CreateTranslator();

var label = await translator.GetTranslation("label.test", Language.Polish);
Console.WriteLine(label);

var translations = await translator.GetAllTranslations(Language.EnglishBritish);
translations.Dump();

var rawJson = await translator.GetAllTranslationsRawJson(Language.German);
rawJson.Dump();