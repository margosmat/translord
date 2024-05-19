using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Reflection;
using translord;
using translord.Core;
using translord.DeepL;
using translord.EntityFramework;
using translord.EntityFramework.Postgres;
using translord.Enums;
using translord.Manager;
using translord.Manager.Data;
using translord.RedisCache;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new ConcurrentDictionary<string, string>());
builder.Services.AddTranslordRedisCache(x =>
{
    x.Server = "localhost";
    x.Port = 6379;
});
builder.Services.AddTranslordFileStore(options =>
{
    options.TranslationsPath = Path.Combine(Directory.GetCurrentDirectory(), "translations");
});
builder.Services.AddTranslordDeepLTranslator(options => { options.AuthKey = builder.Configuration["DeepLAuthKey"]; });
// builder.Services.AddTranslordPostgresStore(options =>
//     options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
// builder.Services.AddTranslordCustomStore<CustomTranslationsStore>();
builder.Services.AddTranslord(o =>
{
    List<Language> supportedLanguages =
    [
        Language.EnglishBritish, Language.Polish, Language.German, Language.French, Language.Japanese, Language.Spanish,
        Language.Ukrainian, Language.Czech
    ];
    o.SupportedLanguages = supportedLanguages;
    o.DefaultLanguage = Language.EnglishBritish;
});

builder.Services.AddDbContext<TranslordManagerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
        b => b.MigrationsAssembly("WebApi")));
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
        List<Language> supportedLanguages = new() { Language.EnglishBritish, Language.Polish };
        var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
        var translator =
            new TranslatorConfiguration(
                new TranslatorConfigurationOptions { SupportedLanguages = supportedLanguages },
                new FileStore(new FileStoreOptions { TranslationsPath = path }, null)).CreateTranslator();

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