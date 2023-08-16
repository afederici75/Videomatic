set migrationName=%1
dotnet ef migrations --startup-project ..\VideomaticRadzen add %migrationName% --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer