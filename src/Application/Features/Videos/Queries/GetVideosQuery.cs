namespace Application.Features.Videos.Queries;

public record GetVideosQuery(
    // IBasicQuery
    string? SearchText = null,
    string? OrderBy = null,
    int? Skip = null,
    int? Take = null,
    TextSearchType? SearchType = null,
    // Additional
    IEnumerable<int>? PlaylistIds = null,
    IEnumerable<int>? VideoIds = null,
    string? SearchOn = null) : IRequest<Page<VideoDTO>>, IBasicQuery;


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

        When(x => x.SearchText is not null, () =>
        {
            // 
            //RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        { 
           RuleFor(x => x.OrderBy).NotEmpty();
        });
        
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Take).GreaterThan(0);
    }
}