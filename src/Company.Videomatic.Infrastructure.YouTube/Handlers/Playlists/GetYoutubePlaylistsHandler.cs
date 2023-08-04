using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.YouTube.Queries;
using Google.Apis.Auth.OAuth2;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Company.Videomatic.Infrastructure.YouTube.Handlers.Playlists;

public class GetYoutubePlaylistsHandler : IRequestHandler<GetYoutubePlaylistsQuery, IEnumerable<PlaylistDTO>>
{
    public GetYoutubePlaylistsHandler(IYouTubeHelper youTubeHelper)
    {
        YouTubeHelper = youTubeHelper ?? throw new ArgumentNullException(nameof(youTubeHelper));
    }

    public IYouTubeHelper YouTubeHelper { get; }

    public async Task<IEnumerable<PlaylistDTO>> Handle(GetYoutubePlaylistsQuery request, CancellationToken cancellationToken)
    {        
        var res = new List<PlaylistDTO>();
        
        await foreach (var pp in YouTubeHelper.GetPlaylistsOfChannel(request.ChannelId))
        {
            res.Add(pp);
        }
        return res;
    }
}