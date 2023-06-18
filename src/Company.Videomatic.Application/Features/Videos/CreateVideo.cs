namespace Company.Videomatic.Application.Features.Videos;

public record CreateVideoCommand(string Location, string Title, string? Description) : IRequest<Video>;