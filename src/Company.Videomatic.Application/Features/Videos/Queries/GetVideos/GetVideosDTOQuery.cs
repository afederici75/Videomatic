namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public record GetVideosDTOQuery(
    int Take = 10, 
    string? TitlePrefix = default, 
    string? DescriptionPrefix = default, 
    string? ProviderIdPrefix = default,
    string? ProviderVideoIdPrefix = default,
    string? VideoUrlPrefix = default,
    int? Skip = 0, 
    string[]? Includes = default, 
    string[]? OrderBy = default) : IRequest<QueryResponse<VideoDTO>>;
