## translord.Manager
translord.Manager contains the TMS admin panel. In this panel you can manage/translate/import translations.

## Configuration
### Web DI
```c#
builder.Services.AddDbContext<TranslordManagerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
        b => b.MigrationsAssembly("WebApi")));
builder.AddTranslordManager();
```
Be sure to run the migrations for the translord.Manager, so that the database can be created:
```bash
dotnet ef migrations add -c TranslordManagerDbContext Init
dotnet ef database update -c TranslordManagerDbContext
```

### Manager configuration 
traslord.Manager reads configuration from the main project appsettings.json, there are 2 options that could be configured:

#### BaseAssemblyName
```json
"BaseAssemblyName": "WebApi"
```

Be sure to add it, so that styles for translord.Manager can be applied correctly.

#### IsTranslordManagerAuthEnabled
```json
"IsTranslordManagerAuthEnabled": true
```

translord.Manager has simple auth implemented from Blazor WebApp template. To use the panel without the auth, leave the property empty, or `false`. If you'd like the auth in the translord.Manager, set it to true. I plan to extend the functionality of the auth in the future, so that there will be role based auth, admin with possibility of adding new users, email account confirmation, etc.