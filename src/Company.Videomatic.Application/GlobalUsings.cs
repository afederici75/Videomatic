global using FluentValidation;
global using MediatR;
global using Hangfire;
global using Ardalis.Result;
global using Ardalis.Specification;
global using Ardalis.GuardClauses;

global using AutoMapper;

global using Company.SharedKernel;
global using Company.SharedKernel.Abstractions;

global using Company.Videomatic.Application.Abstractions;

global using Company.SharedKernel.CQRS.Commands;

global using Company.Videomatic.Application.Model;
global using Company.Videomatic.Domain;

global using Company.Videomatic.Application.Features.Artifacts;
global using Company.Videomatic.Application.Features.Playlists;
global using Company.Videomatic.Application.Features.Transcripts;
global using Company.Videomatic.Application.Features.Videos;

global using Company.Videomatic.Domain.Artifact;
global using Company.Videomatic.Domain.Playlist;
global using Company.Videomatic.Domain.Transcript;
global using Company.Videomatic.Domain.Video;

global using Company.Videomatic.Application.Features.Artifacts.Commands;
global using Company.Videomatic.Application.Features.Playlists.Commands;
global using Company.Videomatic.Application.Features.Transcripts.Commands;
global using Company.Videomatic.Application.Features.Videos.Commands;

global using Company.Videomatic.Application.Features.Artifacts.Queries;
global using Company.Videomatic.Application.Features.Playlists.Queries;
global using Company.Videomatic.Application.Features.Transcripts.Queries;
global using Company.Videomatic.Application.Features.Videos.Queries;
