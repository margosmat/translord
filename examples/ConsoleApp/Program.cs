// See https://aka.ms/new-console-template for more information

using translord;
using translord.Enums;

List<Language> supportedLanguages = new() {Language.English, Language.Polish};
var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
var translator = new TranslatorConfiguration(supportedLanguages, path).CreateTranslator();

Console.WriteLine(translator.GetTranslation("label.test", Language.Polish));

var translations = translator.GetAllTranslations(Language.English);
foreach (var translation in translations)
{
    Console.WriteLine(translation.Value);
}