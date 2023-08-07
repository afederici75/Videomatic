global using FluentValidation;
global using MediatR;
global using Ardalis.Result;
global using Ardalis.Specification;
global using AutoMapper;
global using Company.SharedKernel.Abstractions;
global using Company.Videomatic.Application.Abstractions;

global using Company.SharedKernel.Common;

global using Company.Videomatic.Application.Features.Artifacts;
global using Company.Videomatic.Application.Features.Playlists;
global using Company.Videomatic.Application.Features.Transcripts;
global using Company.Videomatic.Application.Features.Videos;

global using Company.Videomatic.Domain.Aggregates.Artifact;
global using Company.Videomatic.Domain.Aggregates.Playlist;
global using Company.Videomatic.Domain.Aggregates.Transcript;
global using Company.Videomatic.Domain.Aggregates.Video;

global using Company.Videomatic.Application.Features.Artifacts.Commands;
global using Company.Videomatic.Application.Features.Playlists.Commands;
global using Company.Videomatic.Application.Features.Transcripts.Commands;
global using Company.Videomatic.Application.Features.Videos.Commands;

global using Company.Videomatic.Application.Features.Artifacts.Queries;
global using Company.Videomatic.Application.Features.Playlists.Queries;
global using Company.Videomatic.Application.Features.Transcripts.Queries;
global using Company.Videomatic.Application.Features.Videos.Queries;
