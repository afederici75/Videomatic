# Videomatic

Videomatic is a video cataloging application that uses Open AI to analyze videos and produce 
reviews, summaries and much more.

This application is used to demonstrate various software development techniques, including 
Clean Architecture (CA) and Command Query Responsibility Separation (CQRS).

## Prerequisites

## Installation

-We want to install MSSQL on Linux with Full Text Search installed.
-This is a good article:
	https://gianluigi.sellitto.it/2020/03/mssql-server-2019-on-docker-e-full-text-search/
-Steps:

	-Run the following command (might take a minute or two):
```
		docker build -t videomatic/mssql-fts .
```				
	-Run the following command:
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=[...]" -p 1433:1433 --name mssql1 --restart unless-stopped --hostname mssql1 -d videomatic/mssql-fts 
```

-REDIS Stack (Cache and Vector Database)

```
docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 --restart unless-stopped redis/redis-stack:latest
```

-Optional: Install SEQ to capture logs from the application in a friendly UI.

```
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 80:80 -p 5341:5341 datalust/seq
```

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