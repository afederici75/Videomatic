namespace Company.Videomatic.Application.Features.YouTube.Queries;

public record GetYoutubePlaylistsQuery(
    string? SearchText = null) : IRequest<IEnumerable<PlaylistDTO>>;

internal class GetYoutubePlaylistsQueryValidator : AbstractValidator<GetYoutubePlaylistsQuery>
{
    public GetYoutubePlaylistsQueryValidator()
    {
        //When(x => x.SearchText is not null, () =>
        //{
        //    RuleFor(x => x.SearchText).NotEmpty();
        //});
    }
}

