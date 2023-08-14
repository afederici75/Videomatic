﻿using Application.Tests.Helpers;
using Application.Abstractions;
using Application.Features.Videos;

namespace Application.Tests.Validation;

public class VideoRequestsValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public VideoRequestsValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
    }

    [Theory]
    [InlineData(null, null, null, 2)]
    [InlineData("", "", null, 2)]
    [InlineData("http://something/1", null, null, 1)]
    [InlineData("http://somethingElse/2 ", "Video title", "Description", 0)]
    public void ValidateCreateVideoCommand(string location, string title, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<CreateVideoCommandValidator, CreateVideoCommand>(
            CreateVideoCommandBuilder.WithEmptyVideoDetails(location, title, description), expectedErrors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]
    public void ValidateDeleteVideoCommand(int id, int expectedErrors)
    {
        ValidatorHelper.Validate<DeleteVideoCommandValidator, DeleteVideoCommand>(new(id), expectedErrors);
    }

    [Theory]
    [InlineData(-1, null, null, 2)]
    [InlineData(-1, "", null, 2)]
    [InlineData(1, null, null, 1)]
    [InlineData(1, "Play list", null, 0)]
    [InlineData(2, "Play list", "Description", 0)]
    public void ValidateUpdateVideoCommand(int id, string title, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<UpdateVideoCommandValidator, UpdateVideoCommand>(new(id, title, description), expectedErrors);
    }

    [Theory]
    [InlineData(null, "filter_here", "order_here", 1, 1, 0)]
    [InlineData(null, "   ", "", -1, 0, 3)]
    [InlineData(null, null, null, -1, 0, 2)]
    [InlineData(new int[] { }, null, null, -1, 0, 3)]
    public void ValidateGetVideosQuery(
        int[]? playlistIds,
        string? filter,
        string? orderBy,
        int? skip,
        int? take,
        int expectedErrors)
    {
        ValidatorHelper.Validate<GetVideosQueryValidator, GetVideosQuery>(
            new(filter, orderBy, skip, take, TextSearchType.FreeText, playlistIds), expectedErrors);
    }
}