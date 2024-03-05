// See https://aka.ms/new-console-template for more information

using Dumpify;
using translord;
using translord.Enums;

List<Language> supportedLanguages = new() {Language.English, Language.Polish};
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var translator = new TranslatorConfiguration(supportedLanguages, path).CreateTranslator();

Console.WriteLine(translator.GetTranslation("label.test", Language.Polish));

var translations = translator.GetAllTranslations(Language.English);
translations.Dump();

var rawJson = translator.GetAllTranslationsRawJson(Language.Polish);
rawJson.Dump();