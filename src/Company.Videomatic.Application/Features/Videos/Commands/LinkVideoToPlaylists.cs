namespace Company.Videomatic.Application.Features.Videos.Commands;

public record LinkVideoToPlaylistsCommand(long Id, long[] PlaylistIds) : IRequest<LinkVideoToPlaylistsResponse>;

public record LinkVideoToPlaylistsResponse(long VideoId, int count);

internal class LinkVideoToPlaylistsValidator : AbstractValidator<LinkVideoToPlaylistsCommand>
{
    public LinkVideoToPlaylistsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.PlaylistIds).NotEmpty();
        RuleForEach(x => x.PlaylistIds).GreaterThan(0);        
    }
}