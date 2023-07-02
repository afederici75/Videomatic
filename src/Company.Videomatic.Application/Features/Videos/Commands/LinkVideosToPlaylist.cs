namespace Company.Videomatic.Application.Features.Videos.Commands;

public record LinkVideoToPlaylistsCommand(long VideoId, long[] PlaylistIds) : IRequest<LinkVideoToPlaylistsResponse>;

public record LinkVideoToPlaylistsResponse(long VideoId, int count);

internal class LinkVideoToPlaylistsValidator : AbstractValidator<LinkVideoToPlaylistsCommand>
{
    public LinkVideoToPlaylistsValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);

        RuleFor(x => x.PlaylistIds).NotEmpty();
        RuleForEach(x => x.PlaylistIds).GreaterThan(0);        
    }
}