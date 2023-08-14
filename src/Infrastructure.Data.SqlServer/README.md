# Infrastructure.Data.SqlServer

```
dotnet ef migrations --startup-project ..\VideomaticRadZen add Initial --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
```

```
dotnet ef migrations --startup-project ..\VideomaticRadZen add FullTextOnAggregates --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
```

```
dotnet ef database --startup-project ..\VideomaticRadZen update --context SqlServerVideomaticDbContext  -- --Provider SqlServer
```