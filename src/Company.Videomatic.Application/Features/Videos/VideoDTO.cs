namespace Company.Videomatic.Application.Features.Videos;

public record VideoDTO(
    long Id = 0,
    string Location = "",
    string Title = "",
    string? Description = default);