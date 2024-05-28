using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using translord;
using translord.Core;
using translord.DeepL;
using translord.Enums;
using translord.Manager;
using translord.Manager.Data;
using translord.RedisCache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new ConcurrentDictionary<string, string>());

List<Language> supportedLanguages =
[
    Language.EnglishBritish, Language.Polish, Language.German, Language.French, Language.Japanese, Language.Spanish,
    Language.Ukrainian, Language.Czech
];
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
builder.Services.AddTranslord(o =>
{
    o.SupportedLanguages = supportedLanguages;
    o.DefaultLanguage = Language.EnglishBritish;
});

builder.Services.AddDbContext<TranslordManagerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
        b => b.MigrationsAssembly("WebApi")));
builder.AddTranslordManager();

var cache = builder.Services.BuildServiceProvider().GetService<ITranslationsCache>();
if (cache is not null)
{
    await cache.RemoveAll(supportedLanguages.Select(x => $"{x}").ToList());
}

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
        List<Language> languages = [Language.EnglishBritish, Language.Polish];
        var path = Path.Combine(Directory.GetCurrentDirectory(), "translations");
        var translator =
            new TranslatorConfiguration(
                new TranslatorConfigurationOptions { SupportedLanguages = languages },
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