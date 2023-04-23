To install the EF tools
```
dotnet tool install --global dotnet-ef
```

To create migrations run this:
```
dotnet ef migrations --startup-project ..\VideomaticBlazorApp add First
```

To create or update a database run this:
```
dotnet ef database --startup-project ..\VideomaticBlazorApp update
```

To install MSSQL Server 2022 on Linux run this:
```
docker run -e "ACCEPT_EULA=Y" --restart unless-stopped -e "MSSQL_SA_PASSWORD=password" -p 1433:1433 --name mssql1 --hostname mssql1 -d mcr.microsoft.com/mssql/server:2022-latest
```