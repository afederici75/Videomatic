# Company.Videomatic.Infrastructure.Data.Sqlite

```
dotnet ef migrations --startup-project ..\VideomaticWebAPI add Initial --context SqliteVideomaticDbContext  -- --Provider Sqlite
```

```
dotnet ef database --startup-project ..\VideomaticWebAPI update --context SqliteVideomaticDbContext -- --Provider Sqlite
```