# Company.Videomatic.Infrastructure.Data.SqlServer

```
dotnet ef migrations --startup-project ..\VideomaticWebAPI add Initial --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
```


```
dotnet ef database --startup-project ..\VideomaticWebAPI update --context SqlServerVideomaticDbContext  -- --Provider SqlServer
```