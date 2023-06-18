namespace Company.Videomatic.Application.Features.Playlists;

public record GetPlaylistByIdQuery(
    long Id,
    string[]? Includes = default) : IRequest<Playlist?>;

public class GetPlaylistByIdQueryValidator : AbstractValidator<GetPlaylistByIdQuery>
{
    public GetPlaylistByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}