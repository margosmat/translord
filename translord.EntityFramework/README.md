## translord.EntityFramework
translord.EntityFramework contains the configuration of the translations store using EF Core. Be sure to reference packages for your chosen db, and run migrations and db update for the `TranslationsDbContext`.

## Configuration
### Web DI
```c#
services.AddDbContext<TranslationsDbContext>(o => o.UseNpgsql(connectionString)); // or any other db
services.AddTranslordEfStore();
```