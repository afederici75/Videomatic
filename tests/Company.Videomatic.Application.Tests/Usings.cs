global using Xunit;
global using Xunit.DependencyInjection;

global using MediatR;

global using FluentAssertions;

global using Company.SharedKernel.Abstractions;

global using Company.Videomatic.Application.Abstractions;
global using Company.Videomatic.Domain.Model;
global using Company.Videomatic.Infrastructure.Data;
global using Company.Videomatic.Application.Features;
global using Company.Videomatic.Application.Features.Videos.DeleteVideo;

global using Company.Videomatic.Application.Features.Videos.ImportVideo;
global using Company.Videomatic.Application.Features.Videos.UpdateVideo;
global using Company.Videomatic.Application.Features.Videos.GetVideos;
global using Company.Videomatic.Application.Features.Videos.GetTranscript;
global using Company.Videomatic.Application.Specifications;
global using Ardalis.Specification;
global using Company.SharedKernel.Specifications;
