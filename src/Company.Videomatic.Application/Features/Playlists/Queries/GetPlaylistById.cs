using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Application.Features.Playlists.Queries;

public record GetPlaylistByIdQuery(long[] Ids) : IRequest<GetPlaylistByIdResponse>
{
    public GetPlaylistByIdQuery(long Id) : this(new[] { Id })
    { }
};

public record GetPlaylistByIdResponse(IEnumerable<PlaylistDTO> Items);

public class GetPlaylistByIdQueryValidator : AbstractValidator<GetPlaylistByIdQuery>
{
    public GetPlaylistByIdQueryValidator()
    {
        RuleFor(x => x.Ids).NotEmpty();
    }
}