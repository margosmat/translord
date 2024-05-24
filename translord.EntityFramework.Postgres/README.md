## translord.EntityFramework.Postgres

translord.EntityFramework.Postgres contains the configuration of the translations store using EF Core with Postgres.

## Configuration

### Web DI

```c#
builder.Services.AddTranslordPostgresStore(options =>
  options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);// Remember to add connection string to your config, or add it from different place (KeyVault/etc.)
```