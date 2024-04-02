using translord;
using translord.Core;
using translord.EntityFramework;
using translord.EntityFramework.Postgres;
using translord.Enums;
using translord.Manager;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTranslordPostgresStore(options =>
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddTranslordCustomStore<CustomTranslationsStore>();
builder.Services.AddTranslord(o =>
{
    List<Language> supportedLanguages = new() {Language.English, Language.Polish, Language.German};
    o.SupportedLanguages = supportedLanguages;
    o.IsCachingEnabled = true;
});
builder.AddTranslordManager();

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
        var translator =
            new TranslatorConfiguration(
                new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages, IsCachingEnabled = true },
                new FileStore(new FileStoreOptions(path))).CreateTranslator();

        if (key is null) return await translator.GetAllTranslationsRawJson(language);

        return await translator.GetTranslation(key, language);
    })
    .WithName("GetTranslations")
    .WithOpenApi();

app.MapGet("/translations-ef/{language}/{key?}", async (Language language, string? key, ITranslator translator) =>
    {
        if (key is null) return await translator.GetAllTranslationsRawJson(language);

        return await translator.GetTranslation(key, language);
    })
    .WithName("GetTranslationsWithEF")
    .WithOpenApi();

app.UseTranslordManager();

app.Run();