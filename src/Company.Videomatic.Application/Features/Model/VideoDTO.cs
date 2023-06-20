namespace Company.Videomatic.Application.Features.Model;

public record VideoDTO(
    long Id = 0,
    string Location = "",
    string Title = "",
    string? Description = default,

    int? PlaylistCount = 0,
    int? ArtifactCount = 0,
    int? ThumbnailCount = 0,
    int? TranscriptCount = 0,
    int? TagCount = 0,

    ThumbnailDTO? Thumbnail = default);
