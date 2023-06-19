namespace Company.Videomatic.Application.Features.Model;

public record VideoDTO(
    long Id = 0,
    string Location = "",
    string Title = "",
    string? Description = default);
