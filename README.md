# Videomatic

Videomatic is a video cataloging application that uses Open AI to analyze videos and produce 
reviews, summaries and much more.

This application is used to demonstrate various software development techniques, including 
Clean Architecture (CA) and Command Query Responsibility Separation (CQRS).

## Prerequisites

## Installation

## Modules

### Shared Kernel
1. [Shared Kernel](src/Company.SharedKernel/README.md)

### Core
1. [Domain](src/Company.Videomatic.Domain/README.md)
2. [Application](src/Company.Videomatic.Application/README.md)

### Infrastructure
1. [Semantic Kernel](src/Company.Videomatic.Infrastructure.SemanticKernel/README.md)
2. [SQL Server](src/Company.Videomatic.Infrastructure.SqlServer/README.md)
3. [YouTube](src/Company.Videomatic.Infrastructure.YouTube/README.md)
4. [TestData](src/Company.Videomatic.Infrastructure.TestData/README.md)

### Presentation
1. [Blazor](src/VideoMaticBlazorApp/README.md)	

### Tests	
1. [Domain Tests](src/Company.Videomatic.Domain.Tests/README.md)
2. [Application Tests](src/Company.Videomatic.Application.Tests/README.md)
1. [Semantic Kernel Tests](src/Company.Videomatic.Domain.Tests/README.md)
2. [SQL Server Tests](src/Company.Videomatic.Infrastructure.SqlServer.Tests/README.md)
1. [YouTube Tests](src/Company.Videomatic.Infrastructure.YouTube.Tests/README.md)
1. [Integration Tests](src/Company.Videomatic.Integration.Tests/README.md)