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
    })
    .AddTranslordFileStore(options =>
    {
        options.TranslationsPath = Path.Combine(Directory.GetCurrentDirectory(), "translations");
    })
    .AddTranslordDeepLTranslator(options => { options.AuthKey = builder.Configuration["DeepLAuthKey"]; })
    .AddTranslord(o =>
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

app.MapGet("/api/translations/{languageIsoCode}", async (string languageIsoCode, ITranslator translator) =>
    {
        var language = languageIsoCode.ToLower().FromIsoCode();

        return await translator.GetAllTranslationsRawJson(language);
    })
    .WithName("GetTranslations")
    .WithOpenApi();

app.MapGet("/api/supported-languages",
        (ITranslator translator) => translator.GetSupportedLanguages().Select(x => x.GetIsoCode()))
    .WithName("GetSupportedLanguages")
    .WithOpenApi();

app.UseTranslordManager();

app.Run();