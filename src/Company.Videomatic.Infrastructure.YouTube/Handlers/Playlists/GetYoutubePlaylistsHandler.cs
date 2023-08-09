using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.YouTube.Queries;
using Google.Apis.Auth.OAuth2;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Company.Videomatic.Infrastructure.YouTube.Handlers.Playlists;

public class GetYoutubePlaylistsHandler : IRequestHandler<GetYoutubePlaylistsQuery, IEnumerable<GenericPlaylist>>
{
    public GetYoutubePlaylistsHandler(IVideoProvider youTubeHelper)
    {
        YouTubeHelper = youTubeHelper ?? throw new ArgumentNullException(nameof(youTubeHelper));
    }

    public IVideoProvider YouTubeHelper { get; }

    public async Task<IEnumerable<GenericPlaylist>> Handle(GetYoutubePlaylistsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Refactor");
        //var res = new List<GenericPlaylist>();
        //
        //var items = request.ChannelId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        //
        //await foreach (var pp in YouTubeHelper.GetPlaylistInformationAsync(items, cancellationToken))
        //{            
        //    yield return pp;
        //}        
    }
}