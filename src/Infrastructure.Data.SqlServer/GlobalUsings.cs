global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Ardalis.Result;
global using AutoMapper;
global using MediatR;
global using Microsoft.Extensions.Configuration;

global using SharedKernel.Abstractions;
global using Application.Abstractions;

global using Infrastructure.Data;
global using Infrastructure.Data.SqlServer;

global using Infrastructure.Data.Configurations;

global using Application.Features.Artifacts;
global using Application.Features.Playlists;
global using Application.Features.Transcripts;
global using Application.Features.Videos;

global using Domain.Artifacts;
global using Domain.Playlists;
global using Domain.Transcripts;
global using Domain.Videos;

global using Application.Features.Artifacts.Queries;
global using Application.Features.Playlists.Queries;
global using Application.Features.Transcripts.Queries;
global using Application.Features.Videos.Queries;