namespace Company.Videomatic.Application.Features.Playlists;

public record GetPlaylistByIdQuery(
    long Id,
    string[]? Includes = default) : IRequest<GetPlaylistByIdResponse>;

public record GetPlaylistByIdResponse(PlaylistDTO? Item);

public class GetPlaylistByIdQueryValidator : AbstractValidator<GetPlaylistByIdQuery>
{
    public GetPlaylistByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}