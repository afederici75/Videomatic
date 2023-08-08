namespace Company.Videomatic.Application.Features.YouTube.Queries;

public record GetYoutubePlaylistsQuery(
    string ChannelId) : IRequest<IEnumerable<GenericPlaylist>>;

internal class GetYoutubePlaylistsQueryValidator : AbstractValidator<GetYoutubePlaylistsQuery>
{
    public GetYoutubePlaylistsQueryValidator()
    {
        RuleFor(x => x.ChannelId).NotEmpty();        
    }
}

