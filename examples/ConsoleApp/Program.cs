// See https://aka.ms/new-console-template for more information

using Dumpify;
using translord;
using translord.Core;
using translord.Enums;

List<Language> supportedLanguages = [Language.English, Language.Polish];
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var translator =
    new TranslatorConfiguration(
        new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, DefaultLanguage = Language.English },
        new FileStore(new FileStoreOptions { TranslationsPath = path }, null)).CreateTranslator();

var label = await translator.GetTranslation("label.test", Language.Polish);
Console.WriteLine(label);

var translations = await translator.GetAllTranslations(Language.English);
translations.Dump();

var rawJson = await translator.GetAllTranslationsRawJson(Language.German);
rawJson.Dump();