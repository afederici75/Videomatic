﻿namespace Company.Videomatic.Application.Features.Videos;

public record VideoDTO(
    int Id = 0,
    string Location = "",
    string Name = "",
    string? Description = null,
    IEnumerable<string>? Tags = null,
    Thumbnail? Thumbnail = null,
    Thumbnail? Picture = null,
    // 
    int ArtifactCount = 0,
    int TranscriptCount = 0,
    int TagCount = 0,
    //
    string Provider = "",
    string ProviderVideoId = "",
    DateTime VideoPublishedAt = default,
    string VideoOwnerChannelTitle = "",
    string VideoOwnerChannelId = ""
    );