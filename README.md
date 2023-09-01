# Videomatic

Videomatic is a video cataloging application that uses Open AI to analyze videos and produce 
reviews, summaries and much more.

This application is used to demonstrate various software development techniques, including 
Clean Architecture (CA) and Command Query Responsibility Separation (CQRS).

## Prerequisites

1. [Visual Studio Community Edition]
	1. https://visualstudio.microsoft.com/vs/community/	
2. [Docker]
	1. https://www.docker.com/products/docker-desktop		
3. [Microsoft SQL Server Management Studio]
	1. https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms

## Installation

### MSSQL Server 2019 with Full Text Search enabled

1. Build DOCKERFILE to create the image videomatic/mssql-fts (*this might take a few minutes*).
	1. *The image contains MSSQL Server 2019 with Full Text Search enabled*
	2. *More info [here](https://gianluigi.sellitto.it/2020/03/mssql-server-2019-on-docker-e-full-text-search/)*
```
> docker build -t videomatic/mssql-fts .
```

2. Start a container from the image we just created in step 1.
	1. *The container will be named mssql1 and will be listening on port 1433*
	2. **MAKE SURE YOU CHANGE THE PASSWORD**
```
> docker run -e "MSSQL_SA_PASSWORD=[your password here]" "ACCEPT_EULA=Y" -e -p 1433:1433 --name mssql1 --restart unless-stopped --hostname mssql1 -d videomatic/mssql-fts 
```

### REDIS Stack (Cache and Vector Database) in a friendly [web UI](http://localhost:8001/redis-stack/browser).

```
docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 --restart unless-stopped redis/redis-stack:latest
```

### [Optional] SEQ (Logs from the application in a friendly [web UI](http://localhost/#/events).

```
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 80:80 -p 5341:5341 datalust/seq
```

### Database Setup

Now that everything is setup we can create the database and the tables.
Get your command prompt to the folder \src\Infrastructure.Data.SqlServer and run the following command:

```
PS [..]\src\Infrastructure.Data.SqlServer> .\UpdateDb.bat
```

The command should produce something similar to this:

```
PS D:\Videomatic\src\Infrastructure.Data.SqlServer> .\UpdateDb.bat

D:\Videomatic\src\Infrastructure.Data.SqlServer>dotnet ef database --startup-project ..\VideomaticRadzen drop --force --context SqlServerVideomaticDbContext  -- --Provider SqlServer
Build started...
Build succeeded.
[15:58:00 WRN] Sensitive data logging is enabled. Log entries and exception messages may include sensitive application data; this mode should only be enabled during development.
Dropping database 'Videomatic_Tests' on server 'localhost'.
Database 'Videomatic_Tests' did not exist, no action was taken.

D:\Videomatic\src\Infrastructure.Data.SqlServer>dotnet ef database --startup-project ..\VideomaticRadzen update --context SqlServerVideomaticDbContext  -- --Provider SqlServer
Build started...
Build succeeded.
[15:58:06 WRN] Sensitive data logging is enabled. Log entries and exception messages may include sensitive application data; this mode should only be enabled during development.
Applying migration '20230818222029_Initial'.
Applying migration '20230819210049_AddedTopicCategories'.
Applying migration '20230819212449_AddedFulltextIndexing'.
Done.
PS D:\Videomatic\src\Infrastructure.Data.SqlServer>
```

You are now able to use Microsoft SQL Server Management Studio to connect to the database and see the tables.

## Modules

### Shared Kernel
1. [Shared Kernel](src/SharedKernel/README.md)

### Core
1. [Domain](src/Domain/README.md)
2. [Application](src/Application/README.md)

### Infrastructure
1. [Semantic Kernel](src/Infrastructure.SemanticKernel/README.md)
2. [SQL Server](src/Infrastructure.SqlServer/README.md)
3. [YouTube](src/Infrastructure.YouTube/README.md)
4. [Data](src/Infrastructure.Data/README.md)

### Presentation
1. [Blazor](src/VideoMaticBlazorApp/README.md)	
2. [WebAPI](src/VideomaticWebAPI/README.md)	

### Tests	
1. [Domain Tests](tests/Domain.Tests/README.md)
2. [Application Tests](tests/Application.Tests/README.md)
1. [Semantic Kernel Tests](tests/Infrastructure.SemanticKernel.Tests/README.md)
2. [SQL Server Tests](tests/Infrastructure.SqlServer.Tests/README.md)
1. [YouTube Tests](tests/Infrastructure.YouTube.Tests/README.md)
1. [Integration Tests](tests/Integration.Tests/README.md)