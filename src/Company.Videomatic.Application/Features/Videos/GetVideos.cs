namespace Company.Videomatic.Application.Features.Videos;


/// <summary>
/// 
/// </summary>
/// <param name="Take"></param>
/// <param name="TitlePrefix"></param>
/// <param name="DescriptionPrefix"></param>
/// <param name="ProviderIdPrefix"></param>
/// <param name="ProviderVideoIdPrefix"></param>
/// <param name="VideoUrlPrefix"></param>
/// <param name="Skip"></param>
/// <param name="Includes"></param>
/// <param name="OrderBy"></param>
public record GetVideosQuery(
    int? Take = 10,
    string? TitlePrefix = default,
    string? DescriptionPrefix = default,
    string? ProviderIdPrefix = default,
    string? ProviderVideoIdPrefix = default,
    string? VideoUrlPrefix = default,
    int? Skip = 0,
    string[]? Includes = default,
    string[]? OrderBy = default) : IRequest<QueryResponse<GetVideosResult>>;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="ProviderId"></param>
/// <param name="ProviderVideoId"></param>
/// <param name="VideoUrl"></param>
/// <param name="Title"></param>
/// <param name="Description"></param>
/// <param name="Artifacts"></param>
/// <param name="Thumbnails"></param>
/// <param name="Transcripts"></param>

public record GetVideosResult(
    int Id,
    string ProviderId,
    string ProviderVideoId,
    string VideoUrl,
    string? Title,
    string? Description,
    IEnumerable<Artifact>? Artifacts,
    IEnumerable<Thumbnail>? Thumbnails,
    IEnumerable<Transcript>? Transcripts);

// TODO: Should have a DTO for each entity

/// <summary>
/// 
/// </summary>
public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        RuleFor(x => x.Take).GreaterThan(0);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
    }
}