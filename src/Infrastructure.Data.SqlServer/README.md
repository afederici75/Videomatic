# Infrastructure.Data.SqlServer

The SQL Server implementation of the Videomatic data access layer.

# Scaffolding and Migration

```
dotnet ef migrations --startup-project ..\VideomaticRadZen add Initial --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
```

```
dotnet ef migrations --startup-project ..\VideomaticRadZen add FullTextOnAggregates --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
```

```
dotnet ef database --startup-project ..\VideomaticRadZen update --context SqlServerVideomaticDbContext  -- --Provider SqlServer
```