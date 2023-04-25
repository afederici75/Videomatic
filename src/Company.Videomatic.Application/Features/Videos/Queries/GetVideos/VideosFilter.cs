namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public record VideosFilter(int[]? Ids, string? Title, string? Description);
