using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record VideosFilter(
    long[]? PlaylistIds = null,
    string? SearchText = null,
    long[]? Ids = null,
    FilterItem[]? Items = null) : Filter(SearchText, Ids, Items)
{ 
    public VideosFilter(long PlaylistId) : this(PlaylistIds: new[] { PlaylistId }) { }
};


internal class VideosFilterValidator : FilterValidatorBase<VideosFilter>
{
    public VideosFilterValidator()
        : base()
    {
        When(x => x!.PlaylistIds != null, () =>
        {
            RuleFor(x => x.PlaylistIds).NotEmpty();
            RuleForEach(x => x!.PlaylistIds).GreaterThanOrEqualTo(1);
        });
    }

    protected override bool ValidateObjectState(VideosFilter x)
    {
        return base.ValidateObjectState(x) || (x.PlaylistIds != null);
    }
}