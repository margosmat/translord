# WebApiWithUI example
WebApiWithUI uses core `translord` package with `FileStore`, `translord.DeepL` package for AI translations, `translord.RedisCache` for translations caching and `translord.Manager` with UI panel. This example simulates common use case where we have UI app and API that the UI app calls for data.

### Prerequisites
.Net 8
Node.js v20
Redis DB
Postgres DB (when using auth in `translord.Manager`)

### Run
To run the apps please run these commands:

#### Api
```bash
cd WebApi
dotnet restore
dotnet run
```

#### UI
```bash
cd my-ui-app
npm i
npm run dev
```