global using Ardalis.Result;
global using MediatR;
global using Xunit;
global using Xunit.Abstractions;
global using Xunit.DependencyInjection;
global using FluentAssertions;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;

global using Infrastructure.Tests.Data.Helpers;

global using SharedKernel.Abstractions;
global using Application.Features.Playlists;

global using Application.Features.Playlists.Commands;
global using Application.Features.Playlists.Queries;
global using Application.Features.Videos.Commands;
global using Application.Features.Videos.Queries;
global using Infrastructure.Data;
global using Infrastructure.Data.Seeder;

global using Application.Tests.Helpers;

global using Application.Features.Artifacts;
global using Application.Features.Artifacts.Commands;
global using Application.Features.Artifacts.Queries;

global using Domain.Artifacts;
global using Domain.Videos;
global using Domain.Playlists;
global using Domain.Transcripts;

global using Application.Abstractions;
global using Application.Features.Videos;
