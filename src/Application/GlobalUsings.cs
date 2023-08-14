global using FluentValidation;
global using MediatR;
global using Hangfire;

global using Ardalis.Result;
global using Ardalis.Specification;
global using Ardalis.GuardClauses;

global using AutoMapper;

// ------ Videomatic ------ //

global using SharedKernel;
global using SharedKernel.Abstractions;

global using Application.Abstractions;

global using SharedKernel.CQRS.Commands;

global using Application.Model;
global using Domain;

global using Application.Features.Artifacts;
global using Application.Features.Playlists;
global using Application.Features.Transcripts;
global using Application.Features.Videos;

global using Domain.Artifacts;
global using Domain.Playlists;
global using Domain.Transcripts;
global using Domain.Videos;

global using Application.Features.Artifacts.Commands;
global using Application.Features.Playlists.Commands;
global using Application.Features.Transcripts.Commands;
global using Application.Features.Videos.Commands;

global using Application.Features.Artifacts.Queries;
global using Application.Features.Playlists.Queries;
global using Application.Features.Transcripts.Queries;
global using Application.Features.Videos.Queries;
