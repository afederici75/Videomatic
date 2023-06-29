using AutoMapper;
using Company.Videomatic.Domain.Playlists;
using Company.Videomatic.Domain.Videos;

namespace Microsoft.Extensions.DependencyInjection;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        CreateMap<CreatePlaylistCommand, Playlist>();
        CreateMap<UpdatePlaylistCommand, Playlist>();
        
        CreateMap<CreateVideoCommand, Video>();
        CreateMap<CreateVideoDetails, VideoDetails>();

        CreateMap<UpdateVideoCommand, Video>();

        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();
        CreateMap<Thumbnail, ThumbnailDTO>();

        CreateMap<ThumbnailPayload, Thumbnail>();
        CreateMap<TranscriptPayload, Transcript>();
        CreateMap<TranscriptLinePayload, TranscriptLine>();
    }
}
