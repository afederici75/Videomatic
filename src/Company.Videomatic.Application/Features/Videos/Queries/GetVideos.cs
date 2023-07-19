namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    IEnumerable<long>? PlaylistIds = null,
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    bool IncludeTags = false,
    ThumbnailResolutionDTO? IncludeThumbnail = null,
    IEnumerable<long>? VideoIds = null) : IRequest<Page<VideoDTO>>;


internal class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        When(x => x.PlaylistIds is not null, () =>
        {
            RuleFor(x => x.PlaylistIds).NotEmpty();
        });

        When(x => x.VideoIds is not null, () =>
        {
            RuleFor(x => x.VideoIds).NotEmpty();
        }); 

        //When(x => x.SearchText is not null, () =>
        //{
        //    // 
        //    //RuleFor(x => x.SearchText).NotEmpty();
        //});

        When(x => x.OrderBy is not null, () =>
        { 
           RuleFor(x => x.OrderBy).NotEmpty();
        });
        
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}