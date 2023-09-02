# Infrastructure.Data

This project contains the base Entity Framework code which will inherited by provider-specific projects
such Infrastructure.Data.SqlServer

It's important to note that this project does not contain any migrations.

This project was inspired by articles such as:

1. [Migrations with Multiple Providers](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli)
1. [EF Core Migrations with Multiple DB Providers](https://www.meziantou.net/ef-core-migrations-with-multiple-db-providers.htm)
1. [Entity Framework Core and Multiple Database Providers](https://blog.jetbrains.com/dotnet/2022/08/24/entity-framework-core-and-multiple-database-providers/)