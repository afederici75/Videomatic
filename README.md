# Videomatic

[******* THIS README NEEDS A LOT OF WORK *******]

Videomatic is a video cataloging application that uses Open AI to analyze videos and produce 
reviews, summaries and much more.

This application is used to demonstrate various software development techniques, including 
Clean Architecture (CA) and Command Query Responsibility Separation (CQRS).

## Diagrams

Most of the projects in this solution contain one or multiple class diagram files (*.cd) which 
can be used to understand the general direction of the project.

The following diagram shows the high level architecture of the solution.

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/High Level Architecture.png" style="max-height: 450px" />
</div>

The following pictures show more:

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Solution Explorer.png" style="max-height: 250px" />
	<img src="docs/Images/Diagrams/Tests.png" style="max-height: 250px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Domain.png" style="max-height: 450px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Application Model.png" style="max-height: 450px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Features.Artifacts.png" style="max-height: 450px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Features.Playlists.png" style="max-height: 450px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Features.Transcripts.png" style="max-height: 450px" />
</div>

<div style="text-align:center;">
	<img src="docs/Images/Diagrams/Features.Videos.png" style="max-height: 450px" />
</div>

## Prerequisites

1. [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community)
2. [Docker](https://www.docker.com/products/docker-desktop)
3. [*Optional*] [Microsoft SQL Server Management Studio]
	1. https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms

## Installation

The installation of MSSQL is a bit more laborious than it should be, but I wanted to test Full Text Search
first hand. 

I think VM will have 4 search modes at the end:
1. Classic SQL-like queries (SELECT ... FROM ... WHERE ...)
1. [Full Text Search]([url](https://learn.microsoft.com/en-us/sql/relational-databases/search/full-text-search?view=sql-server-ver16) (FTS): FreeText and Contains
1. [Vector Search]([url](https://www.algolia.com/blog/ai/what-is-vector-search/) 
1. [RAG Search (AI)](https://www.youtube.com/watch?v=poRHLfVWg7E)

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

## Configuration and User Secrets

The following projects use user secrets to store sensitive information.
1. Applications (VideomaticRadzen, VideomaticServiceImporter, VideomaticWebAPI)
1. Tests (Infrastructure.Tests)
 
This is an example of such secrets.json file. 

You will need to create your own and replace the values with your own.

```
{
  "ConnectionStrings:Videomatic.SqlServer": "Server=localhost;Database=Videomatic_Tests;User Id=sa;Password=...;TrustServerCertificate=True",
  //"ConnectionStrings:Videomatic.SqlServer": "Server=localhost;Database=Videomatic_BlazorDEV;User Id=sa;Password=...;TrustServerCertificate=True",
  //"ConnectionStrings:Videomatic.SqlServer": "Server=localhost;Database=Videomatic_FullTextTests;User Id=sa;Password=...;TrustServerCertificate=True",

  "YouTube:ServiceAccountEmail": "videomatic@videomatic-384421.iam.gserviceaccount.com",
  "YouTube:CertificatePassword": "...",

  "SemanticKernel:ApiKey": "sk-...",

  "SemanticKernel:MemoryStoreEndpoint": "https://....weaviate.network",
  "SemanticKernel:MemoryStoreApiKey": "BG...",

  "AzureSpeech:ApiKey": "...",
  "AzureSpeech:ServiceRegion": "eastus"
}
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

### Core
1. [Domain](src/Domain/README.md)
2. [Application](src/Application/README.md)

### Infrastructure
1. [Semantic Kernel](src/Infrastructure.SemanticKernel/README.md)
2. [SQL Server](src/Infrastructure.SqlServer/README.md)
3. [YouTube](src/Infrastructure.YouTube/README.md)
4. [Data](src/Infrastructure.Data/README.md)

### Presentation
1. [Blazor](src/VideomaticRadzen/README.md)	
1. [Blazor](src/VideomaticServiceImporter/README.md)	
1. [WebAPI](src/VideomaticWebAPI/README.md)	

### Tests	
1. [Domain Tests](tests/Domain.Tests/README.md)
2. [Application Tests](tests/Application.Tests/README.md)
1. [Infrastructure Tests](tests/Infrastructure.Tests/README.md)

### Shared Kernel
1. [Shared Kernel](src/SharedKernel/README.md)
1. [Shared Kernel for EF Core](src/SharedKernel.EntityFrameworkCore/README.md)

