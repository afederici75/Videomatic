namespace Company.Videomatic.Application.Features.Model;

public record PlaylistDTO(
    long Id = 0,
    string Name = "",
    string? Description = default,
    long VideoCount = 0);
