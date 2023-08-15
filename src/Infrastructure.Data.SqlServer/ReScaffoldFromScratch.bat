rd Migrations /S /Q
dotnet ef migrations --startup-project ..\VideomaticRadzen add Initial --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer
dotnet ef migrations --startup-project ..\VideomaticRadzen add FullTextIndexing --context SqlServerVideomaticDbContext --verbose  -- --Provider SqlServer

ECHO *** COMPLETE *** Don't forget to add a call to 'FullTextIndexingMigrationHelper.Up(migrationBuilder)' in the FullTextIndexing migration.