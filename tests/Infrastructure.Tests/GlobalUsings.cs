global using Ardalis.Result;
global using MediatR;
global using Xunit;
global using Xunit.Abstractions;
global using Xunit.DependencyInjection;
global using FluentAssertions;
global using Microsoft.EntityFrameworkCore;

global using Infrastructure.Tests.Data.Helpers;

global using Company.SharedKernel.Abstractions;
global using Company.Videomatic.Application.Features.Playlists;
global using Company.Videomatic.Domain.Aggregates.Playlist;
global using Company.Videomatic.Application.Features.Playlists.Commands;
global using Company.Videomatic.Application.Features.Playlists.Queries;
global using Company.Videomatic.Application.Features.Videos.Commands;
global using Company.Videomatic.Application.Features.Videos.Queries;
global using Company.Videomatic.Infrastructure.Data;
global using Company.Videomatic.Infrastructure.Data.Seeder;

global using Application.Tests.Helpers;

global using Company.Videomatic.Application.Features.Artifacts;
global using Company.Videomatic.Application.Features.Artifacts.Commands;
global using Company.Videomatic.Application.Features.Artifacts.Queries;
global using Company.Videomatic.Domain.Aggregates.Artifact;
global using Company.Videomatic.Domain.Aggregates.Video;
global using Microsoft.Extensions.Configuration;