using translord;
using translord.Core;
using translord.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/translations/{language}/{key?}", async (Language language, string? key) =>
    {
        List<Language> supportedLanguages = new() {Language.English, Language.Polish};
        var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
        var translator = new TranslatorConfiguration(supportedLanguages, new FileGetter(new FileGetterOptions(path))).CreateTranslator();

        if (key is null) return await translator.GetAllTranslationsRawJson(language);

        return await translator.GetTranslation(key, language);
    })
    .WithName("GetTranslations")
    .WithOpenApi();

app.Run();