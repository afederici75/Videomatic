namespace Company.Videomatic.Application.Features.Videos;

public record VideoDTO(
    long Id = 0,
    string Location = "",
    string Name = "",
    string? Description = null,
    IEnumerable<string>? Tags = null,
    string? Thumbnail = default);